namespace AssessmentAPI.Models
{
    public class CodingAssessmentSubmission
    {

        public Guid CodingAssessmentSubmissionId { get; set; } = Guid.NewGuid();
        public string? Source_code { get; set; }
        public string? Email { get; set; }
        public string? LinkedIn { get; set; }
        public string? Name { get; set; }
        public string? Twitter { get; set; }
        public string? Language { get; set; }
        public Guid? AssessmentId { get; set; }


        //Dev oonly
        public string? Input { get; set; }


    }
}
