using System.Data;

namespace Xphyrus.CreationAPI.Models
{
    public class Coding
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string Language { get; set; }
        public string InputFormat { get; set; }
        public string OutputFormat { get; set; }

        public ICollection<Example> Examples { get; set; }

        public ICollection<COnstraint> Constrains { get; set; }
        public ICollection<MasterCode> Code { get; set; }

        public ICollection<EvliationCase> EvliationCases { get; set; }
    }

    public class EvliationCase
    {   
        public string EvliationCaseId { get; set; }
        public string InputCase { get; set; }
        public string OutputCase { get; set; }
    }

    public class MasterCode
    {
        public int MasterCodeId { get; set; }
        public string Code { get; set; }
        public int Language { get; set; }
    }

    public class COnstraint
    {
        public int COnstraintId { get; set; }
        public string Constraint { get; set; }
    }

    public class Example
    {
        public int ExampleId { get; set; }
        public string  Input { get; set; }
        public string  Output { get; set; }
        public string  Explaination { get; set; }
    }
}