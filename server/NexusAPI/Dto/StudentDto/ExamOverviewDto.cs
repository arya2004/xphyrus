namespace NexusAPI.Dto.StudentDto
{
    public class ExamOverviewDto
    {
        public Guid ExamId { get; set; }
        public string TestTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
    }
}
