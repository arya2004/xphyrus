using NexusAPI.Models;

namespace NexusAPI.Dto
{
    public class TestCaseDto
    {
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
        public string? Description { get; set; }
        public bool IsHidden { get; set; } = false;
        public string CodingQuestionId { get; set; }
        public int Marks { get; set; }

    }
}
