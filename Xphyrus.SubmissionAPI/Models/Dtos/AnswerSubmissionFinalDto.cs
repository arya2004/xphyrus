namespace Xphyrus.SubmissionAPI.Models.Dtos
{
    public class AnswerSubmissionFinalDto
    {
        public string? AssignmentId { get; set; }
        IEnumerable<CodingSubmissionsDto>? CodingSubmissions { get; set; }
        IEnumerable<MCQSubmissionDto>? MCQSubmissions { get; set;}

    }

    public class CodingSubmissionsDto
    {
        public string? source_code { get; set; }
        public int? language_id { get; set; }
      
        public string? CodingId { get; set; }
    }
     public class MCQSubmissionDto
    {
        public string? MCQId { get; set; }
        public int MarkedAnswer { get; set; }
    }

}
