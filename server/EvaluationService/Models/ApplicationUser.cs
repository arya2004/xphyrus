using Microsoft.AspNetCore.Identity;

namespace NexusAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public UserRole Type { get; set; }
        public ICollection<Classroom>? Classrooms { get; set; } = new List<Classroom>();
        public ICollection<StudentAnswerMetadata>? StudentAnswerMetadatas { get; set; } = new List<StudentAnswerMetadata>();
        public string? PRN { get; set; }
        public string? Division { get; set; }
        public string? Batch { get; set; }
        public string? Bio { get; set; }

    }

    public enum UserRole
    {
        Student,
        Teacher,
        Admin
    }







}
