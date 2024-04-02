using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace Xphyrus.ResultAPI.Models
{
    public class CodingAssessment
    {
        [Key]
        public Guid CodingAssessmentId { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }

        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<TestCase>? TestCases { get; set; }

        public Nexus? Nexus { get; set; }
    }

    public class TestCase
    {
        [Key]
        public Guid TestCaseId { get; set; } = Guid.NewGuid();
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
        public CodingAssessment CodingAssessment { get; set; } = null!;
    }
}

  





