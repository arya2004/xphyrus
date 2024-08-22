namespace NexusAPI.Models
{
    public class StudentAnswerMetadata
    {
        public Guid StudentAnswerMetadataId { get; set; } = Guid.NewGuid();
        public ApplicationUser? StudentId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public Test? Test { get; set; }
        public ICollection<StudentAnswer>? StudentAnswers { get; set; } = new List<StudentAnswer>();

    }
}
