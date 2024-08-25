 
//using Azure.Core;
//using EvaluationService.Dtos;
//using EvaluationService.Models;
//using EvaluationService.RabbitMQ;
//using EvaluationService.Service;
//using EvaluationService.Service.IService;
//using Newtonsoft.Json;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;
//using Xphyrus.EvaluationAPI.Service;

//namespace Xphyrus.EvaluationAPI.RabbitMQ
//{
//    public class MQConsumer : BackgroundService
//    {
//        private readonly IConfiguration _configuration;
//        private readonly ResultService _resultService;
//        private IConnection _connection;
//        private IModel _channel;
//        private readonly IMQSender _bus;
//        private IJudgeService _judgeService;
     

//        public MQConsumer(IConfiguration configuration, ResultService resultService, IMQSender bus, IJudgeService judgeService)
//        {
//            _configuration = configuration;
//            _resultService = resultService;
//            _bus = bus;
//            _judgeService = judgeService;
         

//            var factory = new ConnectionFactory
//            {
//                HostName = "localhost",
//                Password = "guest",
//                UserName = "guest",
//            };
//            _connection = factory.CreateConnection();
//            _channel = _connection.CreateModel();
//            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, false, false, null);

//        }
//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            stoppingToken.ThrowIfCancellationRequested();

//            var consumer = new EventingBasicConsumer(_channel);
//            consumer.Received += (sh, ea) =>
//            {
//                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
//                CodingAssessmentSubmission? msg = JsonConvert.DeserializeObject<CodingAssessmentSubmission>(content);

                

//                HandleAsync(msg).GetAwaiter().GetResult();

//                _channel.BasicAck(ea.DeliveryTag, false);

//            };
//            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, consumer);
//            return Task.CompletedTask;
//        }

//        private async Task HandleAsync(CodingAssessmentSubmission msg)
//        {
           
//            JudgeRequest request = new JudgeRequest();
//            request.source_code = msg.Source_code;
//            request.language_id = Int32.Parse(msg.Language);
//            request.stdin = msg.Input;
//            //request.expected_output = "Hello, ArReva";

//            TokenResponse tkres = await _judgeService.SubmitPost(request);



//            Thread.Sleep(10000);
//            SubmissionStatusResponse ress = await _judgeService.GetResponse(tkres);


//            EmailLogger emailLogger = new EmailLogger();

//            emailLogger.To.Add(msg.Email);
//            emailLogger.Subject = "Your Result";
//            emailLogger.Body = BuildMailBody(msg, ress);

//            try
//            {

//                _bus.SendMessage(emailLogger, _configuration.GetValue<string>("TopicAndQueueName:EmailLogging"));
//                _resultService.AddResult(msg, ress).GetAwaiter().GetResult();


//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message.ToString());
       
//            }

            
//        }

//        private string BuildMailBody(CodingAssessmentSubmission submission, SubmissionStatusResponse response)
//        {
//            StringBuilder sb = new StringBuilder();

//            sb.AppendLine("🌟🚀 Hello " + submission.Name + ", 🚀🌟");
//            sb.AppendLine();
//            sb.AppendLine("Thank you for your submission. 🙌");
//            sb.AppendLine("Here is the status of your coding assessment:");
//            sb.AppendLine("------------------------------------------------------");
//            sb.AppendLine("📝 Coding Assessment Submission Details:");
//            sb.AppendLine($"   - Submission ID: {submission.CodingAssessmentSubmissionId}");
//            sb.AppendLine($"   - Source Code: {submission.Source_code}");
//            sb.AppendLine($"   - Email: {submission.Email}");
//            sb.AppendLine($"   - LinkedIn: {submission.LinkedIn}");
//            sb.AppendLine($"   - Name: {submission.Name}");
//            sb.AppendLine($"   - Twitter: {submission.Twitter}");
//            sb.AppendLine($"   - Language: {submission.Language}");
//            sb.AppendLine($"   - Input (Dev only): {submission.Input}");
//            sb.AppendLine();
//            sb.AppendLine("🔍 Submission Status Details:");
//            sb.AppendLine($"   - Stdout: {response.stdout}");
//            sb.AppendLine($"   - Execution Time: {response.time}");
//            sb.AppendLine($"   - Memory Used: {response.memory}");
//            sb.AppendLine($"   - Stderr: {response.stderr}");
//            sb.AppendLine($"   - Token: {response.token}");
//            sb.AppendLine($"   - Compile Output: {response.compile_output}");
//            sb.AppendLine($"   - Message: {response.message}");
//            sb.AppendLine($"   - Status ID: {response.status.id}");
//            sb.AppendLine($"   - Status Description: {response.status.description}");
//            sb.AppendLine("------------------------------------------------------");
//            sb.AppendLine();
//            sb.AppendLine("If you have any questions or concerns, please reach out to us. 📧");
//            sb.AppendLine();
//            sb.AppendLine("Best regards,");
//            sb.AppendLine("Xphyrus 🌌");

//            return sb.ToString();

//        }
//    }
//}
