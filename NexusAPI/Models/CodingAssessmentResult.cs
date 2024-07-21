namespace NexusAPI.Models
{
    public class CodingAssessmentResult
    {
        public Guid CodingAssessmentResultId { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Name { get; set; }
        public string PRN { get; set; }
        public string Division { get; set; }
        public string Batch { get; set; }
        public Guid CodingAssessmentId { get; set; }
        public CodingAssessment CodingAssessment { get; set; }
        public int Score { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ICollection<CodingQuestionResult> CodingQuestionResults { get; set; } = new List<CodingQuestionResult>();
    }

}
