using Docker.DotNet;
using Docker.DotNet.Models;
using EvaluationService.Models;
using EvaluationService.RabbitMQ;
using EvaluationService.Service;
using Newtonsoft.Json;
using NexusAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace Xphyrus.EvaluationAPI.RabbitMQ
{
    public class MQConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
       
        private readonly StudentResultService _studentResultService;
        private readonly CodingQuestionService _codingQuestionService;
        private IConnection _connection;
        private IModel _channel;




        public MQConsumer(IConfiguration configuration, CodingQuestionService codingQuestionService, StudentResultService studentResultService)
        {
            _configuration = configuration;
        
            _codingQuestionService = codingQuestionService;



            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, false, false, null);
            _studentResultService = studentResultService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sh, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                CodingAssessmentSubmission? msg = JsonConvert.DeserializeObject<CodingAssessmentSubmission>(content);



                HandleAsync(msg).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleAsync(CodingAssessmentSubmission msg)
        {
            var executor = new CodeExecutor();
       

            try
            {
                // Fetch the coding question with its test cases
                var codingQuestion = await _codingQuestionService.GetCodingQuestionByIdAsync(msg.QuestionId);

                if (codingQuestion == null)
                {
                    Console.WriteLine("Coding question not found.");
                    return;
                }

                // List to store all StudentAnswers for this submission
                var studentAnswers = new List<StudentAnswer>();

                // Loop through each test case and execute the code
                foreach (var testCase in codingQuestion.TestCases)
                {
                    string output = await executor.ExecuteCodeAsync(msg.Language, msg.Source_code, testCase.InputCase);

                    Console.WriteLine($"Test Case ID: {testCase.TestCaseId}");
                    Console.WriteLine($"Input: {testCase.InputCase}");
                    Console.WriteLine($"Expected Output: {testCase.OutputCase}");
                    Console.WriteLine($"Actual Output: {output}");
                    Console.WriteLine($"Is Correct: {output == testCase.OutputCase}");
                    Console.WriteLine();

                    // Create a StudentAnswer for this test case
                    var studentAnswer = new StudentAnswer
                    {
                        SubmittedCode = msg.Source_code,
                        MarksAwarded = output == testCase.OutputCase ? 100 : 0, // Example scoring logic
                        SubmittedDate = DateTime.Now,
                        CodingQuestion = codingQuestion,
                        //Student = msg.UserId, 
                    };

                    _studentResultService.AddStudentAnswerAsync(new Guid(msg.Metadata), studentAnswer).GetAwaiter().GetResult();
                }

            
           
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine(msg);
        }



    }

    public class CodeExecutor
    {
        private readonly DockerClient _client;

        public CodeExecutor()
        {
            _client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        }

        public async Task<string> ExecuteCodeAsync(string language, string code, string input)
        {
            string? imageName = GetDockerImageName(language);

            if (string.IsNullOrEmpty(imageName))
            {
                throw new ArgumentException("Unsupported language.");
            }

            await PullImageIfNotExistsAsync(imageName);

            string containerId = await CreateContainerAsync(imageName, code, input);

            try
            {
                return await StartContainerAndFetchOutputAsync(containerId);
            }
            finally
            {
                await _client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters() { Force = true });
            }
        }

        private string? GetDockerImageName(string language)
        {
            return language.ToLower() switch
            {
                "c" => "gcc:latest",
                "c++" => "gcc:latest",
                "java" => "openjdk:latest",
                "python" => "python:latest",
                "golang" => "golang:alpine",
                _ => null
            };
        }

        private async Task PullImageIfNotExistsAsync(string imageName)
        {
            var images = await _client.Images.ListImagesAsync(new ImagesListParameters
            {
                Filters = new Dictionary<string, IDictionary<string, bool>>
                {
                    ["reference"] = new Dictionary<string, bool> { [imageName] = true }
                }
            });

            if (!images.Any())
            {
                await _client.Images.CreateImageAsync(new ImagesCreateParameters { FromImage = imageName }, null, new Progress<JSONMessage>());
            }
        }

        private async Task<string> CreateContainerAsync(string imageName, string code, string input)
        {
            var containerCreateParams = new CreateContainerParameters
            {
                Image = imageName,
                Cmd = GetExecutionCommand(imageName, code, input),
                AttachStdout = true,
                AttachStderr = true,
                AttachStdin = !string.IsNullOrEmpty(input),  // Attach stdin only if input is provided
                Tty = false,
            };

            var containerResponse = await _client.Containers.CreateContainerAsync(containerCreateParams);
            string containerId = containerResponse.ID;

            if (!string.IsNullOrEmpty(input))
            {
                var inputData = Encoding.UTF8.GetBytes(input);

                using (var stream = await _client.Containers.AttachContainerAsync(containerId, false, new ContainerAttachParameters { Stdin = true }))
                {
                    // Ensure the container is running before writing to stdin
                    var containerInspect = await _client.Containers.InspectContainerAsync(containerId);
                    if (containerInspect.State.Running)
                    {
                        await stream.WriteAsync(inputData, 0, inputData.Length, CancellationToken.None);
                        // Properly handle the EOF signal to the process
                        stream.CloseWrite();  // Close the write side of the pipe
                    }
                }
            }

            return containerId;
        }

        private string[] GetExecutionCommand(string imageName, string code, string input)
        {
            return imageName switch
            {
                "gcc:latest" => new[] { "sh", "-c", $"echo '{code}' > /tmp/main.cpp && g++ /tmp/main.cpp -o /tmp/a.out && echo '{input}' | /tmp/a.out" },
                "openjdk:latest" => new[] { "sh", "-c", $"echo '{code}' > /tmp/Main.java && javac /tmp/Main.java && echo '{input}' | java -cp /tmp Main" },
                "python:latest" => new[] { "sh", "-c", $"echo '{input}' | python -c \"{code}\"" },
                "golang:alpine" => new[] { "sh", "-c", $"echo '{code}' > /tmp/main.go && go build -o /tmp/main /tmp/main.go && echo '{input}' | /tmp/main" },
                _ => throw new ArgumentException("Unsupported image.")
            };
        }


        private async Task<string> StartContainerAndFetchOutputAsync(string containerId)
        {
            await _client.Containers.StartContainerAsync(containerId, new ContainerStartParameters());

            var logs = await _client.Containers.GetContainerLogsAsync(containerId, new ContainerLogsParameters
            {
                ShowStdout = true,
                ShowStderr = true,
                Follow = true
            });

            using (var reader = new StreamReader(logs))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}

