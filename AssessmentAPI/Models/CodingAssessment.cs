using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssessmentAPI.Models
{
    /// <summary>
    /// Represents a coding assessment.
    /// </summary>
    public class CodingAssessment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the coding assessment.
        /// </summary>
        [Key]
        public Guid CodingAssessmentId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the title of the coding assessment.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the coding assessment.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the start date of the coding assessment.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the coding assessment.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of test cases associated with the coding assessment.
        /// </summary>
        public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();

        /// <summary>
        /// Gets or sets the Nexus associated with the coding assessment.
        /// </summary>
        public Nexus? Nexus { get; set; }
    }

    /// <summary>
    /// Represents a test case for a coding assessment.
    /// </summary>
    public class TestCase
    {
        /// <summary>
        /// Gets or sets the unique identifier for the test case.
        /// </summary>
        [Key]
        public Guid TestCaseId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the input case for the test case.
        /// </summary>
        public string? InputCase { get; set; }

        /// <summary>
        /// Gets or sets the expected output case for the test case.
        /// </summary>
        public string? OutputCase { get; set; }

        /// <summary>
        /// Gets or sets the coding assessment to which the test case belongs.
        /// </summary>
        public CodingAssessment CodingAssessment { get; set; } = null!;
    }
}
