using EvaluationService.Dtos;
using EvaluationService.RabbitMQ;

namespace EvaluationService.Service.IService
{
    public interface IResultService
    {
        Task AddResult(CodingAssessmentSubmission codingAssessmentSubmission);
        public Task<ResponseDto> GetAllForAssessment(Guid assessmentId);

        Task Migrate();
    }
}
