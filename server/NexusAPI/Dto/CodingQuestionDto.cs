using NexusAPI.Models;

namespace NexusAPI.Dto
{
    public class CodingQuestionDto
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public Difficulty Difficulty { get; set; }

        public Guid TestId { get; set; }
    }
}
