using NexusAPI.Models;

namespace EvaluationService.Service.IService
{
    public interface IStudentResultService
    {
        Task AddStudentAnswerAsync(Guid studentAnswerMetadataId, StudentAnswer studentAnswer);
    }

}
