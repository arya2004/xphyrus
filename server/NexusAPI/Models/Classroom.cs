namespace NexusAPI.Models
{
    public class Classroom
    {
        public Guid ClassroomId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public CourseType Type { get; set; }

        public ApplicationUser? Teacher { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;

        public ICollection<Test>? Tests { get; set; } = new List<Test>();

    }


    public enum CourseType
    {
        Theory = 1,
        Tutorial = 2,
        Lab = 3,

    }



}
