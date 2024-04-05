namespace SubmissionAPI.Dtos
{
    public class SubmissionRequest
    {

        public string? source_code { get; set; }
        public int? language_id { get; set; }

        public string? stdin { get; set; }
        public string? expected_output { get; set; }
        public int StudentId { get; set; }
        public int AssesmentId { get; set; }

        public SubmissionRequest()
        {

        }
        public SubmissionRequest(TestRunDto testRunDto)
        {
            source_code = testRunDto.source_code;
            language_id = testRunDto.language_id;
            stdin = testRunDto.stdin;
            expected_output = testRunDto.exprected_output;
        }
    }
}
