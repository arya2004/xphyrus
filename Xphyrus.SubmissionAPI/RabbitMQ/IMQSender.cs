namespace Xphyrus.SubmissionAPI.RabbitMQ
{
    public interface IMQSender
    {   
        void SendMessage(string Message, string QueueName);
    }
}
