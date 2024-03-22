using Microsoft.AspNetCore.Identity;

namespace Xphyrus.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? WebsiteUrl { get; set; }
    }
}
