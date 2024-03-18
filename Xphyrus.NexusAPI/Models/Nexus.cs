namespace Xphyrus.NexusAPI.Models
{
    public class Nexus
    {
        public Guid NexusId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Guid Creator { get; set; }
        public ICollection<Guid>? Admins { get; set; }
        public DateTime CreationDate { get; set; }
     
        public ICollection<CodingAssessment> CodingAssessments { get; set; }
    }
}
