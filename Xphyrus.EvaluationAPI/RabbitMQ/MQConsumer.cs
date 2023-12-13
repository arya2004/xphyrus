
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Xphyrus.EvaluationAPI.Models;
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
                String msg = JsonConvert.DeserializeObject<string>(content);

                HandleAsync(msg).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleAsync(string msg)
        {
            Models.Dtos.SubmissionRequest a = new Models.Dtos.SubmissionRequest();
            a.source_code = msg;
            a.stdin = msg;
            a.language_id = 5;
            
            _resultService.AddResult(a).GetAwaiter().GetResult();
        }
    }
}
