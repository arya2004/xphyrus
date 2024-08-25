using EvaluationService.Dtos;
using EvaluationService.Models;
using EvaluationService.RabbitMQ;

namespace EvaluationService.Service.IService
{
    public interface IResultService
    {
        Task AddResult(CodingAssessmentSubmission codingAssessmentSubmission, SubmissionStatusResponse submissionStatusResponse);
        public Task<ResponseDto> GetAllForAssessment( Guid assessmentId);

        Task Migrate();
    }
}
