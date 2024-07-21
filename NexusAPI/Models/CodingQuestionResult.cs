

namespace NexusAPI.Models
{
    public class CodingQuestionResult
    {
        public Guid CodingQuestionResultId { get; set; } = Guid.NewGuid();
        public string SourceCode { get; set; }
        public string Time { get; set; }
        public int Memory { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public Guid CodingAssessmentResultId { get; set; }
        public CodingAssessmentResult CodingAssessmentResult { get; set; }
        public Guid CodingQuestionId { get; set; }
        public CodingQuestion CodingQuestion { get; set; }
    }

}
