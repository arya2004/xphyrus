
using Newtonsoft.Json;
using NexusAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;

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
                EmailDetails? msg = JsonConvert.DeserializeObject<EmailDetails>(content);

                HandleAsync(msg).GetAwaiter().GetResult();

                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(_configuration.GetValue<string>("TopicAndQueueName:EmailService"), false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleAsync(EmailDetails emailDetails)
        {
            try
            {
                // Select the template based on the intent
                string? templatePath = GetTemplatePath(emailDetails.Intent);
                if (string.IsNullOrEmpty(templatePath))
                {
                    throw new Exception("No template found for the specified intent.");
                }

                // Read the HTML template
                string emailBody = await File.ReadAllTextAsync(templatePath);

                // Replace placeholders in the template with actual values from the Info dictionary
                foreach (var kvp in emailDetails.Info)
                {
                    Console.WriteLine("{{" + $"{kvp.Key}" + "}}");
                    emailBody = emailBody.Replace("{{" + $"{kvp.Key}" + "}}", kvp.Value);
                }

                // Prepare the email message
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(username, "Xphyrus"), // Specify the sender's email and display name

                    Subject = emailDetails.Subject,
                    Body = emailBody,
                    IsBodyHtml = true
                };

                // Add recipients
                foreach (var to in emailDetails.To)
                {
                    mailMessage.To.Add(to);
                }

                foreach (var cc in emailDetails.CC)
                {
                    mailMessage.CC.Add(cc);
                }

                foreach (var bcc in emailDetails.Bcc)
                {
                    mailMessage.Bcc.Add(bcc);
                }

                // Configure the SMTP client
                using (var smtpClient = new SmtpClient(smtpServer))
                {
                    smtpClient.Port = 587; // or 25, or the port your SMTP server uses
                    smtpClient.Credentials = new NetworkCredential(username, password);
                    smtpClient.EnableSsl = true;

                    // Send the email
                    await smtpClient.SendMailAsync(mailMessage);
                }

                await Task.Delay(2000); // Delay between sending nvalidOperationException: A from address must be specified.emails, if necessary
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw;
            }
        }


        private static string? GetTemplatePath(string intent)
        {
            string templateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates");

            return intent switch
            {
                "confirm-email" => Path.Combine(templateDirectory, "confirm-email.html"),
                "reset-password" => Path.Combine(templateDirectory, "reset-password.html"),
                "result-declaration" => Path.Combine(templateDirectory, "result-declaration.html"),
                _ => null
            };
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
