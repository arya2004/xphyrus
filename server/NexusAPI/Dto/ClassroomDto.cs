using NexusAPI.Models;

namespace NexusAPI.Dto
{
    public class ClassroomDto
    {
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public CourseType Type { get; set; }

    }
}
