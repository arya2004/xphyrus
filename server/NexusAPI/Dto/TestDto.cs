using NexusAPI.Models;

namespace NexusAPI.Dto
{
    public class TestDto
    {

        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public string? ClassroomId { get; set; }
    }
}
