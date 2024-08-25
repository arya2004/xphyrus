﻿namespace EvaluationService.Dtos
{
    public class SubmissionStatusResponse
    {
        public string? stdout { get; set; }
        public string? time { get; set; }
        public long memory { get; set; }
        public string? stderr { get; set; }
        public string? token { get; set; }
        public string? compile_output { get; set; }
        public string? message { get; set; }
        public Status status { get; set; }
    }

    public class Status
    {
        public int? id { get; set; }
        public string? description { get; set; }
    }
}
