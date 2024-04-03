namespace SubmissionAPI.Models.Dtos
{
    public class TestRunDto
    {
        public string? source_code { get; set; }
        public int? language_id { get; set; }
        public string? stdin { get; set; }
        public string? exprected_output { get; set; }
    }
}
