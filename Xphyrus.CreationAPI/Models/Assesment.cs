using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Xphyrus.AssesmentAPI.Models
{
    public class Assesment
    {
        [Key]
        public string AssesmentId { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public string?  Code { get; set; }
        public bool IsStrict { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Duration { get; set; }
        public Coding? Codings { get; set; }

    }

  

   
}
