
using Azure.Core;
using EvaluationService.Dtos;
using EvaluationService.Models;
using EvaluationService.RabbitMQ;
using EvaluationService.Service;
using EvaluationService.Service.IService;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
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
        private readonly IMQSender _bus;



        public MQConsumer(IConfiguration configuration, ResultService resultService, IMQSender bus)
        {
            _configuration = configuration;
            _resultService = resultService;
            _bus = bus;
     


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
            string resultOutput = "";
            string sourceFilePath = "";
            string outputFilePath = "";

            // Define file extensions and commands based on language
            string fileExtension = "";
            string compileCommand = "";
            string executeCommand = "";

            switch (msg.Language.ToLower())
            {
                case "c":
                    fileExtension = ".c";
                    sourceFilePath = $"temp{fileExtension}";
                    outputFilePath = "temp.exe";
                    compileCommand = $"gcc {sourceFilePath} -o {outputFilePath}";
                    executeCommand = $"{outputFilePath}";
                    break;

                case "c++":
                    fileExtension = ".cpp";
                    sourceFilePath = $"temp{fileExtension}";
                    outputFilePath = "temp.exe";
                    compileCommand = $"g++ {sourceFilePath} -o {outputFilePath}";
                    executeCommand = $"{outputFilePath}";
                    break;

                case "c#":
                    fileExtension = ".cs";
                    sourceFilePath = $"temp{fileExtension}";
                    outputFilePath = "temp.exe";
                    compileCommand = $"csc {sourceFilePath} -out:{outputFilePath}";
                    executeCommand = $"{outputFilePath}";
                    break;

                case "java":
                    fileExtension = ".java";
                    sourceFilePath = "Main.java";
                    outputFilePath = "Main";
                    compileCommand = $"javac {sourceFilePath}";
                    executeCommand = $"java {outputFilePath}";
                    break;

                case "python":
                    fileExtension = ".py";
                    sourceFilePath = $"temp{fileExtension}";
                    executeCommand = $"python {sourceFilePath}";
                    break;

                default:
                    throw new Exception("Unsupported language");
            }

            // Write the source code to a file
            await File.WriteAllTextAsync(sourceFilePath, msg.Source_code);

            try
            {
                if (!string.IsNullOrEmpty(compileCommand))
                {
                    // Compile the source code (if necessary)
                    var compileProcess = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c {compileCommand}",
                            RedirectStandardOutput = true,
                            RedirectStandardError = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        }
                    };

                    compileProcess.Start();
                    string compileResult = await compileProcess.StandardOutput.ReadToEndAsync();
                    string compileError = await compileProcess.StandardError.ReadToEndAsync();
                    compileProcess.WaitForExit();

                    if (compileProcess.ExitCode != 0)
                    {
                        throw new Exception($"Compilation failed: {compileError}");
                    }
                }

                // Execute the compiled file or script
                var executeProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c {executeCommand}",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                executeProcess.Start();

                if (!string.IsNullOrEmpty(msg.Input))
                {
                    await executeProcess.StandardInput.WriteLineAsync(msg.Input);
                }

                resultOutput = await executeProcess.StandardOutput.ReadToEndAsync();
                string executionError = await executeProcess.StandardError.ReadToEndAsync();
                executeProcess.WaitForExit();

                if (executeProcess.ExitCode != 0)
                {
                    throw new Exception($"Execution failed: {executionError}");
                }

            }
            catch (Exception ex)
            {
                resultOutput = ex.Message;
            }
            finally
            {
                // Cleanup
                if (File.Exists(sourceFilePath))
                {
                    File.Delete(sourceFilePath);
                }
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
            }

            // Prepare the email
            EmailLogger emailLogger = new EmailLogger();
            emailLogger.To.Add(msg.Email);
            emailLogger.Subject = "Your Result";
            emailLogger.Body = resultOutput;
            Console.WriteLine(resultOutput.ToString());
            Console.WriteLine(msg);

            try
            {
                //_bus.SendMessage(emailLogger, _configuration.GetValue<string>("TopicAndQueueName:EmailLogging"));
                //_resultService.AddResult(msg, resultOutput).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


    }
}
