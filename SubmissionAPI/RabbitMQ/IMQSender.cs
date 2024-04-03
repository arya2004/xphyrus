using SubmissionAPI.Models;

namespace SubmissionAPI.RabbitMQ
{
    public interface IMQSender
    {
        void SendMessage(CodingAssessmentSubmission Message, string QueueName);
    }
}
