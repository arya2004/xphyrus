namespace EvaluationService.Dtos
{
    public class SubmissionRequest
    {
        public string? source_code { get; set; }
        public int? language_id { get; set; }
        public string? stdin { get; set; }
        public string? expected_output { get; set; }
        public int StudentId { get; set; }
        public int AssesmentId { get; set; }


    }
}
