namespace NexusAPI.Dto.StudentDto
{
    public class SubmitQuestionDto
    {

        public Guid CodingAssessmentSubmissionId { get; set; } = Guid.NewGuid();


        public string? Source_code { get; set; }

        public string? Language { get; set; }

        public Guid? QuestionId { get; set; }
        public Guid? TestId { get; set; }

    }

}
