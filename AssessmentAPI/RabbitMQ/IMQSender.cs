using AssessmentAPI.Models;

namespace AssessmentAPI.RabbitMQ
{
    /// <summary>
    /// Interface for sending messages to a message queue.
    /// </summary>
    public interface IMQSender
    {
        /// <summary>
        /// Sends a message to a specified queue.
        /// </summary>
        /// <param name="message">The message to send, represented as a <see cref="CodingAssessmentSubmission"/>.</param>
        /// <param name="queueName">The name of the queue to which the message should be sent.</param>
        void SendMessage(CodingAssessmentSubmission message, string queueName);
    }
}
