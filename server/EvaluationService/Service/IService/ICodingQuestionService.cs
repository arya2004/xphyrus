using NexusAPI.Models;

namespace EvaluationService.Service.IService
{
    public interface ICodingQuestionService
    {
        Task<CodingQuestion?> GetCodingQuestionByIdAsync(Guid codingQuestionId);
    }
}
