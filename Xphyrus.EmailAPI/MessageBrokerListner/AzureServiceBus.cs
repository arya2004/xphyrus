using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using Xphyrus.EmailAPI.Models;

namespace Xphyrus.EmailAPI.MessageBrokerListner
{
    public class AzureServiceBus : IAzureServiceBus
    {
        private readonly string _serviceName;
        private readonly string _queueName;
        private readonly IConfiguration _configuration;
        private readonly string smtpServer = "smtp.office365.com";
        private readonly int smtpPort = 587;
        private readonly string username = "xphyrus@outlook.com";
        private readonly string password = "!Ziegler00601221";

        private ServiceBusProcessor _processor;
        public AzureServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceName = _configuration.GetValue<string>("ServiceBusConnectionString");
            _queueName = _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions");

            var client = new ServiceBusClient(_serviceName);

            _processor = client.CreateProcessor(_queueName);

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

            EmailLogger? email = JsonConvert.DeserializeObject<EmailLogger>(body);
            try
            {
               foreach (var item in email.To)
                {
                    // neu insance always, causse this is in singleton
                    SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                    smtpClient.EnableSsl = true; 

                  
                    smtpClient.Credentials = new NetworkCredential(username, password);

                 
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(username); 
                    mailMessage.To.Add(item);
                    


                
                    mailMessage.Subject = email.Subject;
                    //mailMessage.IsBodyHtml = true;
                    mailMessage.Body = email.Body;

                    try
                    {

                       await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    finally
                    {
                        smtpClient?.Dispose();
                    }
                    await Task.Delay(2000);

                }
                
               

                Console.WriteLine(email);
                await arg.CompleteMessageAsync(arg.Message);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
