
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Mail;
using System.Net;
using System.Text;
using EmailService.Models;

namespace EmailService.RabbitMQ
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
            _channel.QueueDeclare(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, false, false, null);
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
            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"), false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleAsync(EmailLogger email)
        {
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
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                    finally
                    {
                        smtpClient?.Dispose();
                    }
                    await Task.Delay(2000);

                }



                Console.WriteLine(email);
             
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
//{
//    "To": ["arya20j4@gmail.com"],
//  "Cc": ["arya20j4@gmail.com", "arya.pathak22@vit.edu"],
//  "Bcc": ["arya.pathak2004@gmail.com"],
//  "Subject": "Example Subject",
//  "Body": "This is an example email body."
//}
