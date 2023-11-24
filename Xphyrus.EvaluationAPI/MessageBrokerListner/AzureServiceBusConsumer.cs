using Azure.Core;
using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using Xphyrus.EvaluationAPI.Models.Dtos;
using Xphyrus.EvaluationAPI.Service;
using Xphyrus.EvaluationAPI.Service.IService;

namespace Xphyrus.EvaluationAPI.MessageBrokerListner
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string _serviceBusConnectionSring;
        private readonly string _queueName;
        private readonly IConfiguration _configuration;
        private ServiceBusProcessor _processor;
        private readonly ResultService _resultService;
        private readonly IHttpClientFactory _httpClientFactory;
        public AzureServiceBusConsumer(IConfiguration configuration, ResultService resultService, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _serviceBusConnectionSring = _configuration.GetValue<string>("ServiceBusConnectionString");
            _queueName = _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions");

            var client = new ServiceBusClient(_serviceBusConnectionSring);

            _processor = client.CreateProcessor(_queueName);
        
            _resultService = resultService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Start()
        {
            _processor.ProcessMessageAsync += OnSubmissionReceived;
            _processor.ProcessErrorAsync += ErrorHandler;
            await _processor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnSubmissionReceived(ProcessMessageEventArgs arg)
        {
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            SubmissionRequest objmessage = JsonConvert.DeserializeObject<SubmissionRequest>(body);
            try
            {
                SubmissionRequest temp = new SubmissionRequest()
                {
                    source_code = objmessage.source_code,
                    language_id = objmessage.language_id,
                    stdin = objmessage.stdin ?? "dotnet meow"
                };
                var httpClient = _httpClientFactory.CreateClient();
                var uri = new Uri("https://coding-classroom.com/judge/submissions/");
                var json = JsonConvert.SerializeObject(temp);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {

                    // Print response body
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                else
                {
                    
                }
                //try to log email
                await _resultService.AddResult(objmessage);
                Console.WriteLine(objmessage);
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
