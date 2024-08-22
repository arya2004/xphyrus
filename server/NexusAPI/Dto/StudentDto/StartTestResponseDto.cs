using NexusAPI.Models;

namespace NexusAPI.Dto.StudentDto
{
    public class StartTestResponseDto
    {
        public Test Test { get; set; }
        public Guid StudentAnswerMetadataId { get; set; }
    }
}
