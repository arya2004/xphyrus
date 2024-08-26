namespace EvaluationService
{
    public class JudgeConfig
    {

        public const int number_of_runs = 1;

        public const int cpu_time_limit = 5;
        public const int cpu_extra_time = 1;
        public const int wall_time_limit = 10;
        public const int memory_limit = 128000;
        public const int stack_limit = 64000;
        public int max_processes_and_or_threads = 60;
        public bool enable_per_process_and_thread_time_limit = true;
        public bool enable_per_process_and_thread_memory_limit = true;
        public int max_file_size = 1024;

    }
}


//public string? stdout { get; set; }
//public string? time { get; set; }
//public long memory { get; set; }
//public string? stderr { get; set; }
//public string? token { get; set; }
//public string? compile_output { get; set; }
//public string? message { get; set; }
//public Status status { get; set; }