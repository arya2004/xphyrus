namespace EvaluationService.Models
{
    public class CodingAssessmentSubmission
    {

        public Guid CodingAssessmentSubmissionId { get; set; }
        public string? SourceCode { get; set; }
        public int? LanguageId { get; set; }

        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? College { get; set; }


        public DateTime? CreatedON { get; set; }

        public Guid? AssessmentId { get; set; }

    }
}
