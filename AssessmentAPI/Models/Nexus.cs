using System;
using System.Collections.Generic;

namespace AssessmentAPI.Models
{
    /// <summary>
    /// Represents a Nexus entity.
    /// </summary>
    public class Nexus
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Nexus.
        /// </summary>
        public Guid NexusId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the Nexus.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the Nexus.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the creator of the Nexus.
        /// </summary>
        public Guid? Creator { get; set; }

        /// <summary>
        /// Gets or sets the list of application users associated with the Nexus.
        /// </summary>
        public List<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();

        /// <summary>
        /// Gets or sets the creation date of the Nexus.
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the collection of coding assessments associated with the Nexus.
        /// </summary>
        public ICollection<CodingAssessment> CodingAssessments { get; set; } = new List<CodingAssessment>();
    }
}
