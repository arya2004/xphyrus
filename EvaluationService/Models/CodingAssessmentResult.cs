using EvaluationService.Dtos;

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

        public Guid AssessmentId { get; set; }



        public CodingAssessmentResult()
        {
            
        }

        public CodingAssessmentResult(CodingAssessmentSubmission codingAssessmentSubmission, SubmissionStatusResponse submissionStatusResponse)
        {
            Source_code = codingAssessmentSubmission.Source_code;
            Email = codingAssessmentSubmission.Email;
            LinkedIn = codingAssessmentSubmission.LinkedIn;
            Name = codingAssessmentSubmission.Name;
            Twitter = codingAssessmentSubmission.Twitter;
            Language = codingAssessmentSubmission.Language;

            Time = submissionStatusResponse.time;
            Memory = submissionStatusResponse.memory;
            Message = submissionStatusResponse.message;
            Description = submissionStatusResponse.status.description;

        }


    }
}
