
using Azure.Core;
using EvaluationService.Dtos;
using EvaluationService.Models;
using EvaluationService.RabbitMQ;
using EvaluationService.Service;
using EvaluationService.Service.IService;
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
        private IJudgeService _judgeService;
     
        private readonly IHttpClientFactory _httpClientFactory;
        public MQConsumer(IConfiguration configuration, ResultService resultService, IMQSender bus, IHttpClientFactory httpClientFactory, IJudgeService judgeService)
        {
            _configuration = configuration;
            _resultService = resultService;
            _bus = bus;
            _judgeService = judgeService;
         
            _httpClientFactory = httpClientFactory;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, false, false, null);
            _httpClientFactory = httpClientFactory;
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
           
            JudgeRequest request = new JudgeRequest();
            request.source_code = msg.Source_code;
            request.language_id = Int32.Parse(msg.Language);
            request.stdin = msg.Input;
            //request.expected_output = "Hello, ArReva";

             TokenResponse tkres = await _judgeService.SubmitPost(request);
            
            var httpClient = _httpClientFactory.CreateClient();
            var uri = new Uri("http://localhost:2358/submissions/");
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            TokenResponse? res = JsonConvert.DeserializeObject<TokenResponse>(responseBody);


            Thread.Sleep(10000);
            SubmissionStatusResponse statusResponse = await _judgeService.GetResponse(tkres);

            var client = _httpClientFactory.CreateClient("Judge0");
            var resp = await client.GetAsync($"/submissions/" + res.token.ToString());
            var apiContent = await resp.Content.ReadAsStringAsync();
            var ress = JsonConvert.DeserializeObject<SubmissionStatusResponse>(apiContent);

            EmailLogger emailLogger = new EmailLogger();

            emailLogger.To.Add(msg.Email);
            emailLogger.Subject = "Your Result";
            emailLogger.Body = ress.stdout.ToString() + "\n\n\n" + request.source_code.ToString();

            try
            {

                _bus.SendMessage(emailLogger, _configuration.GetValue<string>("TopicAndQueueName:EmailLogging"));
             

            }
            catch (Exception ex)
            {

       
            }

           // _resultService.AddResult(a).GetAwaiter().GetResult();
        }
    }
}
