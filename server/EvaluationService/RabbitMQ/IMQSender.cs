
using EvaluationService.Models;

namespace EvaluationService.RabbitMQ
{
    public interface IMQSender
    {
        void SendMessage(EmailLogger Message, string QueueName);
    }
}
