namespace SubmissionAPI.RabbitMQ
{
    public interface IMQSender
    {
        void SendMessage(string Message, string QueueName);
    }
}
