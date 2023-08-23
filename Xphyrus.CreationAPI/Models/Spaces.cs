using Microsoft.AspNetCore.Identity;

namespace Xphyrus.CreationAPI.Models
{
    public class Spaces
    {
        public Spaces()
        {
            
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string  Code { get; set; }
        public bool IsStrict { get; set; }
        public ICollection<IdentityUser> Admins { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Coding> Codings { get; set; }
    }

   
}
