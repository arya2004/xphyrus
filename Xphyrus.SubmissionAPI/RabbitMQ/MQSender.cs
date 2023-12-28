

using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Xphyrus.SubmissionAPI.RabbitMQ
{
    
    public class MQSender : IMQSender
    {
        private readonly string _hostName;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        public MQSender()
        {
            _hostName = "localhost";
            _username = "guest";
            _password = "guest";
        }
        public void SendMessage(string Message, string QueueName)
        {
            if (ConnectionExist())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(QueueName, false, false, false, null);
                var json = JsonConvert.SerializeObject(Message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: QueueName, null, body: body);
            }

      
       
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    Password = _password,
                    UserName = _username,
                };
                //  
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool ConnectionExist()
        {
            if(_connection != null)
            {
                return true;
            }
            CreateConnection();
            return true;
        }
    }
}
