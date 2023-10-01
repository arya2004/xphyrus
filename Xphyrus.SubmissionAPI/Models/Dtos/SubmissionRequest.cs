namespace Xphyrus.SubmissionAPI.Models.Dtos
{
    public class SubmissionRequest
    {
        
        public string? source_code { get; set; }
        public int? language_id { get; set; }
        public int? number_of_runs = JudgeConfig.number_of_runs;
        public string? stdin { get; set; }
        public string? expected_output { get; set; }
        public int? cpu_time_limit = JudgeConfig.cpu_time_limit;
        public int? cpu_extra_time = JudgeConfig.cpu_extra_time;
        public int? wall_time_limit = JudgeConfig.wall_time_limit;
        public int? memory_limit = JudgeConfig.memory_limit;
        public int? stack_limit = JudgeConfig.stack_limit;
        public int? max_processes_and_or_threads = JudgeConfig.max_processes_and_or_threads;
        public bool? enable_per_process_and_thread_time_limit = JudgeConfig.enable_per_process_and_thread_time_limit;
        public bool? enable_per_process_and_thread_memory_limit = JudgeConfig.enable_per_process_and_thread_time_limit;
        public int? max_file_size = JudgeConfig.max_file_size;
        public bool? enable_network = JudgeConfig.enable_per_process_and_thread_time_limit;
        public SubmissionRequest(TestRunDto testRunDto)
        {
            this.source_code = testRunDto.source_code;
            this.language_id = testRunDto.language_id;
            this.stdin = testRunDto.stdin;
            this.expected_output = testRunDto.exprected_output;
        }
    }
}
