namespace NexusAPI.Models
{
    public class Test
    {

        public Guid TestId { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public Classroom? Classroom { get; set; }
        public ICollection<CodingQuestion>? CodingQuestions { get; set; } = new List<CodingQuestion>();
        public ICollection<StudentAnswerMetadata>? StudentAnswerMetadatas { get; set; } = new List<StudentAnswerMetadata>();
    }
}
