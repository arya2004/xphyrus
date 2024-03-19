namespace Xphyrus.NexusAPI.Models
{
    public class Admin
    {

        public Guid AdminId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Nexus Nexus{ get; set; }
    }
}
