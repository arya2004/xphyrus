
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Net;
using System.Text;
using NexusAPI.Models;

namespace NexusAPI.RabbitMQ
{
    public class MQConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;

        private IConnection _connection;
        private IModel _channel;
        private readonly string smtpServer = "smtp.office365.com";
        private readonly int smtpPort = 587;
        private readonly string username = "xphyrus@outlook.com";
        private readonly string password = "!Ziegler00601221";

        public MQConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Password = "guest",
                UserName = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueName:EmailService"), false, false, false, null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sh, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                EmailLogger? msg = JsonConvert.DeserializeObject<EmailLogger>(content);

                HandleAsync(msg).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:EmailService"), false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleAsync(EmailLogger email)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(username, password);

                    foreach (var item in email.To)
                    {
                        using (MailMessage mailMessage = new MailMessage())
                        {
                            mailMessage.From = new MailAddress(username);
                            mailMessage.To.Add(item);
                            mailMessage.Subject = email.Subject;
                            mailMessage.Body = email.Body;

                            try
                            {
                                await smtpClient.SendMailAsync(mailMessage);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error sending email to {item}: {ex.Message}");
                                // Optionally, log the error or handle it accordingly
                                throw;
                            }
                        }

                        await Task.Delay(2000); // Delay between sending emails, if necessary
                    }
                }

                Console.WriteLine("Emails sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw;
            }
        }
    }
}
//{
//    "to": ["arya20j4@gmail.com"],
//  "cc": ["arya20j4@gmail.com", "arya.pathak22@vit.edu"],
//  "bcc": ["arya.pathak2004@gmail.com"],
//  "subject": "example subject",
//  "body": "this is an example email body."
//}
