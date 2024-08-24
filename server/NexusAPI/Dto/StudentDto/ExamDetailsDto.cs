namespace NexusAPI.Dto.StudentDto
{
    public class ExamDetailsDto
    {
        public Guid ExamId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public ICollection<StudentAnswerDto> StudentAnswers { get; set; }
    }

    public class StudentAnswerDto
    {
        public string SubmittedCode { get; set; }
        public int MarksAwarded { get; set; }
        public DateTime SubmittedDate { get; set; }
        public string QuestionText { get; set; }
    }

    public class DetailTestDto
    {
        public Guid TestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<DetailCodingQuestionDto> CodingQuestions { get; set; }
    }

    public class DetailCodingQuestionDto
    {
        public Guid CodingQuestionId { get; set; }
        public string QuestionText { get; set; }
        public int MaxMarks { get; set; }
    }

}
