
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

                EmailLogger emailLogger = new EmailLogger();
                
                emailLogger.To.Add(msg.Email);
                emailLogger.Subject = "SUbmission Noted";
                emailLogger.Body = msg.Email;

                HandleAsync(emailLogger).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleAsync(EmailLogger msg)
        {
            //SubmissionRequest a = new SubmissionRequest();
            //a.source_code = msg;
            //a.stdin = msg;
            //a.language_id = 5;


            try
            {

                _bus.SendMessage(msg, _configuration.GetValue<string>("TopicAndQueueName:EmailLogging"));
             

            }
            catch (Exception ex)
            {

       
            }

           // _resultService.AddResult(a).GetAwaiter().GetResult();
        }
    }
}
