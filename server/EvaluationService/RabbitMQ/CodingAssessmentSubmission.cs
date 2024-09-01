namespace EvaluationService.RabbitMQ
{
    public class CodingAssessmentSubmission
    {
        /// <summary>
        /// Gets or sets the source code submitted by the user.
        /// </summary>
        public string Source_code { get; set; }

        /// <summary>
        /// Gets or sets the language of the source code (e.g., "c", "c++", "java", "python").
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the input to be provided to the program during execution.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user who submitted the code.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets any additional metadata or information needed for the submission.
        /// </summary>
        public string Metadata { get; set; }

        // Additional properties can be added as necessary
        public Guid TestId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
    }

}
