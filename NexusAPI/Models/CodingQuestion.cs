namespace NexusAPI.Models
{
    public class CodingQuestion
    {
        public Guid CodingQuestionId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CodingAssessmentId { get; set; }
        public CodingAssessment CodingAssessment { get; set; }
        public string Difficulty { get; set; }
     
        public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
    }

}
