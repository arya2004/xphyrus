using System.ComponentModel.DataAnnotations;

namespace Xphyrus.AssesmentAPI.Models.Dto
{
    public class CodingDto
    {

        public string? Title { get; set; }
        public string? Prompt { get; set; }
        public string? Language { get; set; }
        public string? InputFormat { get; set; }
        public string? OutputFormat { get; set; }

        public string Constrain1 { get; set; }
        public string Constrain2 { get; set; }
        public string Constrain3 { get; set; }


        public ICollection<EvliationCaseDto>? EvliationCases { get; set; }
    }

    public class EvliationCaseDto
    {

        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
    }

    public class MasterCodeDto
    {

        public string? Code { get; set; }
        public int Language { get; set; }
    }

    public class COnstraintDto
    {

        public string? Constraint { get; set; }
    }

    public class ExampleDto
    {

        public string? Input { get; set; }
        public string? Output { get; set; }
        public string? Explaination { get; set; }
    }

}
