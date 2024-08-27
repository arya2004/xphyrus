namespace NexusAPI.Dto.StudentDto
{
    public class StudentTestDto
    {
        public Guid TestId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public ICollection<StudentQuestionDto>? CodingQuestions { get; set; }

    }
}
