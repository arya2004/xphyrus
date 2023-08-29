using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Xphyrus.CreationAPI.Models
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

        public ICollection<Example>? Examples { get; set; }

        public ICollection<COnstraint>? Constrains { get; set; }
        public ICollection<MasterCode>? Code { get; set; }

        public ICollection<EvliationCase>? EvliationCases { get; set; }
    }

    public class EvliationCase
    {   
        [Key] 
        public string EvliationCaseId { get; set; } = Guid.NewGuid().ToString();
        public string InputCase { get; set; }
        public string OutputCase { get; set; }
    }

    public class MasterCode
    {
        [Key] 
        public string MasterCodeId { get; set; } = Guid.NewGuid().ToString();
        public string? Code { get; set; }
        public int Language { get; set; }
    }

    public class COnstraint
    {
        [Key]
        public string COnstraintId { get; set; } = Guid.NewGuid().ToString();
        public string? Constraint { get; set; }
    }

    public class Example
    {
        [Key] 
        public string ExampleId { get; set; } = Guid.NewGuid().ToString();
        public string?  Input { get; set; }
        public string?  Output { get; set; }
        public string?  Explaination { get; set; }
    }
}