using NexusAPI.Models;

namespace NexusAPI.Dto.StudentDto
{
    public class StudentQuestionDto
    {
        public Guid CodingQuestionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public Difficulty Difficulty { get; set; }

        public ICollection<StudentTestCaseDto>? TestCases { get; set; }


    }

    public class StudentTestCaseDto
    {
        public Guid TestCaseId { get; set; }
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
        public string? Description { get; set; }
        public bool IsHidden { get; set; } = false;

    }
}
