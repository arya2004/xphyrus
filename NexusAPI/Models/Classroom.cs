using Azure;
using Humanizer;

namespace NexusAPI.Models
{
    public class Classroom
    {
        public Guid ClassroomId { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public CourseType Type { get; set; }
        public string FacultyId { get; set; }
        public ApplicationUser Faculty { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsArchived { get; set; } = false;
        public string EnrollmentKey { get; set; }
        public int MaxStudents { get; set; }
        public ICollection<CodingAssessment> CodingAssessments { get; set; } = new List<CodingAssessment>();
    }


    public enum CourseType
    {
        Theory = 1,
        Tutorial = 2,
        Lab = 3,
        
    }



}
