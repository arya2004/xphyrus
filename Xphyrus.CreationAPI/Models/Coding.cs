using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Xphyrus.AssesmentAPI.Models
{
    public class Coding
    {
        [Key]
        public string CodingId { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public string? Prompt { get; set; }
        public string? Language { get; set; }
        public string? InputFormat { get; set; }
        public string? OutputFormat { get; set; }
        public string? Constrain1 { get; set; }
        public string? Constrain2 { get; set; }
        public string? Constrain3 { get; set; }

      
        public ICollection<EvliationCase>? EvliationCases { get; set; }


        public string? AssesmentId { get; set; }
        [ForeignKey("AssesmentId")]
        public Assesment? Assesment { get; set; }
    }

    public class EvliationCase
    {   
        [Key] 
        public string EvliationCaseId { get; set; } = Guid.NewGuid().ToString();
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }

        public string? CodingId { get; set; }
        [ForeignKey("CodingId")]
        public Coding? Coding { get; set; }
    }

   
  
   
}