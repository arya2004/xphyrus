using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Xphyrus.CreationAPI.Models
{
    public class Assesment
    {
        
        public int AssesmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string  Code { get; set; }
        public bool IsStrict { get; set; }
        public ICollection<AssesmentAdmin> Admins { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Coding> Codings { get; set; }

    }

    public class AssesmentAdmin
    {
        
        public int AssesmentAdminId { get; set; }
        public string Id { get; set; }
    }

   
}
