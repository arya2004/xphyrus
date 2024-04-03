namespace SubmissionAPI.Models.Dtos
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
            this.source_code = testRunDto.source_code;
            this.language_id = testRunDto.language_id;
            this.stdin = testRunDto.stdin;
            this.expected_output = testRunDto.exprected_output;
        }
    }
}
