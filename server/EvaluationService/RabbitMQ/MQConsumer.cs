using Docker.DotNet;
using Docker.DotNet.Models;
using EvaluationService.Models;
using EvaluationService.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Xphyrus.EvaluationAPI.Service;

namespace Xphyrus.EvaluationAPI.RabbitMQ
{
    public class MQConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ResultService _resultService;
        private IConnection _connection;
        private IModel _channel;




        public MQConsumer(IConfiguration configuration, ResultService resultService)
        {
            _configuration = configuration;
            _resultService = resultService;




            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, false, false, null);

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
            string output = await executor.ExecuteCodeAsync(msg.Language, msg.Source_code, msg.Input);

            // Prepare the email
            EmailLogger emailLogger = new EmailLogger();
            emailLogger.To.Add(msg.Email);
            emailLogger.Subject = "Your Result";
            emailLogger.Body = output;
            Console.WriteLine(output);
            Console.WriteLine(msg);

            try
            {

                //_resultService.AddResult(msg, resultOutput).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }





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
            string imageName = GetDockerImageName(language);

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

        private string GetDockerImageName(string language)
        {
            return language.ToLower() switch
            {
                "c" => "gcc:latest",
                "c++" => "gcc:latest",
                "java" => "openjdk:latest",
                "c#" => "mcr.microsoft.com/dotnet/sdk:latest",
                "python" => "python:latest",
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
                Cmd = GetExecutionCommand(imageName, code),
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

        private string[] GetExecutionCommand(string imageName, string code)
        {
            return imageName switch
            {
                "gcc:latest" => new[] { "sh", "-c", $"echo '{code}' > /tmp/main.cpp && g++ /tmp/main.cpp -o /tmp/a.out && /tmp/a.out" },
                "openjdk:latest" => new[] { "sh", "-c", $"echo '{code}' > /tmp/Main.java && javac /tmp/Main.java && java -cp /tmp Main" },
                "mcr.microsoft.com/dotnet/sdk:latest" => new[] { "sh", "-c", $"echo '{code}' > /tmp/Program.cs && dotnet run /tmp/Program.cs" },
                "python:latest" => new[] { "python", "-c", code },
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

