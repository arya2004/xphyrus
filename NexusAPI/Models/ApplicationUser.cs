using Azure;
using Microsoft.AspNetCore.Identity;

namespace NexusAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? CompanyName { get; set; }
        public string? WorkEmail { get; set; }
        
        public string? Location { get; set; }
        public string? Market { get; set; }
        public string? OneLinePitch { get; set; }
        public CompanySize CompanySize { get; set; }

        // socials
        public string? WebsiteUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string?  LinkedinUrl { get; set; }

        //user
        public string? Role { get; set; }

        public List<Nexus>? Nexus { get; }
      
    }

    public enum CompanySize
    {
        Small_1_10 = 1,
        Small_11_50 = 2,
        Medium_51_200 = 3,
        Medium_201_500 = 4,
        Large_501_1000 = 5,
        Large_1001_5000 = 6,
        Enterprise_5000Plus = 7
    }



}
