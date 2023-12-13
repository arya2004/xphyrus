

namespace Xphyrus.SubmissionAPI.RabbitMQ
{
    
    public class MQSender : IMQSender
    {
        private readonly string _hostName;
        private readonly string _username;
        private readonly string _password;
        public MQSender()
        {
            _hostName = "localhost";
            _username = "guest";
            _username = "guest";
        }
        public void SendMessage(string Message, string QueueName)
        {   
          //  var factory = new
          //  
        
       
        }
    }
}
