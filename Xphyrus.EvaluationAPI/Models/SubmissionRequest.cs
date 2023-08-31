namespace Xphyrus.EvaluationAPI.Models
{
    public class SubmissionRequest
    {
        public string SubmissionRequestId { get; set; } = Guid.NewGuid().ToString();
        public string? source_code { get; set; }
        public int? language_id { get; set; }
        public int? number_of_runs { get; set; }
        public string? stdin { get; set; }
        public string? expected_output { get; set; }
        public int? cpu_time_limit { get; set; }
        public int? cpu_extra_time { get; set; }
        public int? wall_time_limit { get; set; }
        public int? memory_limit { get; set;}
        public int? stack_limit { get; set; }
        public int? max_processes_and_or_threads { get; set; }
        public bool? enable_per_process_and_thread_time_limit { get; set; }
        public bool? enable_per_process_and_thread_memory_limit { get; set; }
        public int? max_file_size { get; set; }
        public bool? enable_network { get; set; }
    }
}
