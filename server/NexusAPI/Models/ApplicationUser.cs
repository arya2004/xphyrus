using Azure;
using Microsoft.AspNetCore.Identity;

namespace NexusAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
        public string ProfilePictureUrl { get; set; }
        public string Bio { get; set; }
       
    }

    public enum UserRole
    {
        Student,
        Faculty,
        Admin
    }







}
