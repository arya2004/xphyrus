using System.ComponentModel.DataAnnotations;

namespace Xphyrus.AssesmentAPI.Models
{
    public class AssesmentParticipant
    {
        [Key]
        public string AssesmentParticipantId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string? ApplicationUser { get; set; }
        [Required]
        public string? AssesmentId { get; set; }
        [Required]
        public bool HasStarted { get; set; }
        [Required]
        public bool HasCompleted { get; set; }
    }
}
