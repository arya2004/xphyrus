using EvaluationService.Models;

namespace EvaluationService.Service.IService
{
    public interface IResultService
    {
        Task AddResult(SubmissionRequest submissionRequest);

        Task Migrate();
    }
}
