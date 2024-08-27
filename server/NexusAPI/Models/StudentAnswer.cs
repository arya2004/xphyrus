namespace NexusAPI.Models
{
    public class StudentAnswer
    {
        public Guid StudentAnswerId { get; set; } = Guid.NewGuid();
        public string? SubmittedCode { get; set; }
        public int MarksAwarded { get; set; }
        public DateTime SubmittedDate { get; set; } = DateTime.Now;
        public CodingQuestion? CodingQuestion { get; set; }
        public ApplicationUser? Student { get; set; }
    }
}
