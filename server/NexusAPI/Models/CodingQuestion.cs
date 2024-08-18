namespace NexusAPI.Models
{
    public class CodingQuestion
    {
        public Guid CodingQuestionId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
      
        public Test? Test { get; set; }
       
        public Difficulty Difficulty { get; set; }
     
        public ICollection<TestCase>? TestCases { get; set; } = new List<TestCase>();
        public ICollection<StudentAnswer>? StudentAnswers { get; set; } = new List<StudentAnswer>();
    }

    public enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3,

    }
}



