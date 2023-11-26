using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Xphyrus.AssesmentAPI.Models;

namespace Xphyrus.AssesmentAPI.Models
{
    //public class Coding
    //{
    //    [Key]
    //    public string CodingId { get; set; } = Guid.NewGuid().ToString();
    //    public string? Title { get; set; }
    //    public string? Prompt { get; set; }
    //    public string? Language { get; set; }
    //    public string? InputFormat { get; set; }
    //    public string? OutputFormat { get; set; }
    //    public string? Constrain1 { get; set; }
    //    public string? Constrain2 { get; set; }
    //    public string? Constrain3 { get; set; }

      
    //    public ICollection<EvliationCase>? EvliationCases { get; set; }


    //    public string? AssesmentId { get; set; }
    //    [ForeignKey("AssesmentId")]
    //    public Assesment? Assesment { get; set; }
    //}

    //public class EvliationCase
    //{   
    //    [Key] 
    //    public string EvliationCaseId { get; set; } = Guid.NewGuid().ToString();
    //    public string? InputCase { get; set; }
    //    public string? OutputCase { get; set; }

    //    public string? CodingId { get; set; }
    //    [ForeignKey("CodingId")]
    //    public Coding? Coding { get; set; }
    //}

    public class EvaluationCase
    {
        [Key]
        public string EvaluationCaseId { get; set; } = Guid.NewGuid().ToString();
        public string Input { get; set; }
        public string Output { get; set; }
        public string? CodingId { get; set; }
        [ForeignKey("CodingId")]
        public CodingAssesment? Coding { get; set; }
    }
}

    public class CodingAssesment
    {
        [Key]
         public string CodingAssesmentId { get; set; } = Guid.NewGuid().ToString();
         public string Title { get; set; }
        public string Description { get; set; }
        public string JoinCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<EvaluationCase> EvaluationCases { get; set; }
    }




