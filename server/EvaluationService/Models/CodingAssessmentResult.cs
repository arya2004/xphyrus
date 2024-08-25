﻿using EvaluationService.Dtos;

namespace EvaluationService.Models
{
    public class CodingAssessmentResult
    {
        
        public Guid CodingAssessmentResultId { get; set; } = Guid.NewGuid();
        public string? Source_code { get; set; }
        public string? Email { get; set; }
        public string? LinkedIn { get; set; }
        public string? Name { get; set; }
        public string? Twitter { get; set; }
        public string? Language { get; set; }

        public string? Time { get; set; }
        public int? Memory { get; set; }
   

        public string? Message { get; set; }
        public string? Description { get; set; }

        public Guid? AssessmentId { get; set; }



        public CodingAssessmentResult()
        {
            
        }

        


    }
}
