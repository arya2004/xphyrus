namespace SubmissionAPI.Models
{
    public class CodingAssessmentSubmission
    {

        public Guid CodingAssessmentSubmissionId { get; set; } = Guid.NewGuid();
        public string? SourceCode { get; set; }
        public int? LanguageId { get; set; }

        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? College { get; set; }


        public DateTime CreatedON { get; set; } = DateTime.UtcNow;

        public Guid? AssessmentId { get; set; }
    
    }
}
