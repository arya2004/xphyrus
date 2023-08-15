namespace Xphyrus.CreationAPI.Models.Dto
{
    public class UserSpacesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsStrict { get; set; }
     
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<MCQDto>? MCQs { get; set; }
        public List<MSQDto>? MSQs { get; set; }
        public List<FITBDto>? FITBs { get; set; }
        public List<EssayDto>? Essays { get; set; }
        public List<CodingDto>? Codings { get; set; }
    }

  

    public class EssayDto
    {
        public string Paragraph { get; set; }
    }

    public class FITBDto
    {
        public string IncompleteSentence { get; set; }
        public string[]? Choices { get; set; }
    }

    public class MSQDto
    {
        public string Question { get; set; }
        public string[] Choices { get; set; }
    }

    public class MCQDto
    {
        public string Question { get; set; }
        public string[] Choices { get; set; }
    }

    public class CodingDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string Language { get; set; }
        public string[] InputFormat { get; set; }
        public string[] OutputFormat { get; set; }

        public Example[] Examples { get; set; }

        public COnstraint[] Constrains { get; set; }
       

       
    }

   



    public class COnstraint
    {
        public string Constraint { get; set; }
    }

    public class Example
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string Explaination { get; set; }
    }
}
