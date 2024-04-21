namespace EvaluationService.Models
{
    public class TestCase
    {
     
        public Guid TestCaseId { get; set; }
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
        public Guid CodingAssessment { get; set; }
    }
}
