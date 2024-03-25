using Azure;

namespace Xphyrus.NexusService.Models
{
    public class Nexus
    {
        public Guid NexusId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Guid? Creator { get; set; }
        public List<ApplicationUser>? ApplicationUsers { get; } 
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ICollection<CodingAssessment>? CodingAssessments { get; set; }
    }
}
