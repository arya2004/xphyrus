

namespace Xphyrus.NexusAPI.Models.Dto
{
    public class AssesmentParticipantDto
    {
   
        public string? ApplicationUserEmail { get; set; }
        public string? AssesmentId { get; set; }
  
        public bool HasStarted { get; set; }

        public bool HasCompleted { get; set; }
    }
}
