using System.ComponentModel.DataAnnotations;

namespace NexusAPI.Dto.TeacherDto
{
    public class ClassroomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description length can't be more than 500.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "TeacherId is required.")]
        public string TeacherId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
