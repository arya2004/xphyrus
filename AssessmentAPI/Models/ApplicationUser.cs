using Azure;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AssessmentAPI.Models
{
    /// <summary>
    /// Represents a user in the application.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the URL of the user's website.
        /// </summary>
        public string? WebsiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of Nexus associated with the user.
        /// </summary>
        public List<Nexus> Nexus { get; set; } = new List<Nexus>();
    }
}
