using System.ComponentModel.DataAnnotations.Schema;

namespace Xphyrus.AssesmentAPI.Models.Dto
{
    public class MCQDto
    {
        public string? Question { get; set; }
        public int CorrectAnswer { get; set; }
        public ICollection<OptionsDto>? Options { get; set; }

    }

    public class OptionsDto
    {
        public int OptionNumber { get; set; }
        public string? Value { get; set; }
    }
}
