using NexusAPI.Models;

namespace NexusAPI.Dto.StudentDto
{
    public class StartTestResponseDto
    {
        public StudentTestDto Test { get; set; }
        public Guid StudentAnswerMetadataId { get; set; }
    }
}
