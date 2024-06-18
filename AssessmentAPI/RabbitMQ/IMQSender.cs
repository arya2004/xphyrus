using AssessmentAPI.Models;

namespace AssessmentAPI.RabbitMQ
{
    public interface IMQSender
    {
        void SendMessage(CodingAssessmentSubmission Message, string QueueName);
    }
}
