namespace EvaluationService.Models.Dtos
{
    public class JudgeRequest
    {
        public string? source_code { get; set; }
        public int? language_id { get; set; }
        public string? stdin { get; set; }
        public string? expected_output { get; set; }
    }
}
