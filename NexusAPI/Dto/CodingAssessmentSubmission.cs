using System;

namespace NexusAPI.Dto
{
    /// <summary>
    /// Represents a submission for a coding assessment.
    /// </summary>
    public class CodingAssessmentSubmission
    {
        /// <summary>
        /// Gets or sets the unique identifier for the coding assessment submission.
        /// </summary>
        public Guid CodingAssessmentSubmissionId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the source code submitted for the assessment.
        /// </summary>
        public string? Source_code { get; set; }

        /// <summary>
        /// Gets or sets the email of the submitter.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the LinkedIn profile of the submitter.
        /// </summary>
        public string? LinkedIn { get; set; }

        /// <summary>
        /// Gets or sets the name of the submitter.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the Twitter handle of the submitter.
        /// </summary>
        public string? Twitter { get; set; }

        /// <summary>
        /// Gets or sets the programming language used for the submission.
        /// </summary>
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the assessment associated with the submission.
        /// </summary>
        public Guid? AssessmentId { get; set; }

        /// <summary>
        /// Gets or sets the input data for the submission (for development purposes).
        /// </summary>
        /// <remarks>This property is for development purposes only.</remarks>
        public string? Input { get; set; }
    }
}
