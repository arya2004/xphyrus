using NexusAPI.Dto.StudentDto;

namespace NexusAPI.Dto.TeacherDto
{
    public class StudentAnswerMetadataDto
    {
        public Guid StudentAnswerMetadataId { get; set; }
        public string StudentName { get; set; } // Assuming you want to include the student's name
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public TestDto Test { get; set; }
        public List<StudentAnswerDto> StudentAnswers { get; set; }
    }
}
