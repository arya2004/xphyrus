using Newtonsoft.Json;
using NexusAPI.Dto;
using RabbitMQ.Client;
using System.Text;

namespace NexusAPI.RabbitMQ
{
    /// <summary>
    /// Implementation of <see cref="IMQSender"/> for sending messages to RabbitMQ.
    /// </summary>
    public class MQSender : IMQSender
    {
        private readonly string _hostName;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MQSender"/> class with default RabbitMQ connection settings.
        /// </summary>
        public MQSender()
        {
            _hostName = "localhost";
            _username = "guest";
            _password = "guest";
        }

        /// <summary>
        /// Sends a message to a specified queue.
        /// </summary>
        /// <param name="message">The message to send, represented as a <see cref="CodingAssessmentSubmission"/>.</param>
        /// <param name="queueName">The name of the queue to which the message should be sent.</param>
        public void SendMessage(CodingAssessmentSubmission message, string queueName)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "Message cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new ArgumentException("Queue name cannot be null or empty.", nameof(queueName));
            }

            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queueName, false, false, false, null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
            else
            {
                throw new InvalidOperationException("Failed to establish a connection to RabbitMQ.");
            }
        }

        /// <summary>
        /// Creates a connection to RabbitMQ.
        /// </summary>
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _username,
                    Password = _password,
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                // Log the exception details for further analysis (not implemented here)
                // For example: _logger.LogError(ex, "Error occurred while creating RabbitMQ connection.");
                throw new InvalidOperationException("Could not create a connection to RabbitMQ.", ex);
            }
        }

        /// <summary>
        /// Checks if a connection to RabbitMQ exists, and creates one if it does not.
        /// </summary>
        /// <returns>True if the connection exists, otherwise false.</returns>
        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();
            return _connection != null;
        }
    }
}
