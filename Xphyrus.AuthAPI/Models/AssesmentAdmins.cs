using System.ComponentModel.DataAnnotations;

namespace Xphyrus.AuthAPI.Models
{
    public class AssesmentAdmins
    {
        [Key]
        public string AssesmentAdminsId { get; set; } = Guid.NewGuid().ToString();
        [Required]

        public string? ApplicationUser { get; set; }
        [Required]
        public string? AssesmentId { get; set; }
        [Required]
        public bool HasResultDeclared { get; set; }
    }
}
