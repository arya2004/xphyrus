using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Models
{
    public class TestCase
    {
        public Guid TestCaseId { get; set; } = Guid.NewGuid();
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
        public string? Description { get; set; }
        public bool IsHidden { get; set; } = false;
        public CodingQuestion? CodingQuestion { get; set; }
        public int Marks { get; set; }
    }

}
