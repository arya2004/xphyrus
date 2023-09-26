using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xphyrus.AssesmentAPI.Models
{
    public class MCQ
    {
        public string MCQId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string?  Question { get; set; }
        public int CorrectAnswer { get; set; }
        [Required]
        public ICollection<Options>? Options { get; set; }

        public string? AssesmentId { get; set; }
        [ForeignKey("AssesmentId")]
        public Assesment? Assesment { get; set; }

    }

    public class Options
    {
        public string OptionsId { get; set; } = Guid.NewGuid().ToString();
        public int OptionNumber { get; set; }
        public string? Value { get; set; }

        public string? MCQId { get; set; }
        [ForeignKey("MCQId")]
        public MCQ? MCQ { get; set; }
    }
}
