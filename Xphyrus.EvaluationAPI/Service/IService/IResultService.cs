using Xphyrus.EvaluationAPI.Models.Dtos;

namespace Xphyrus.EvaluationAPI.Service.IService
{
    public interface IResultService
    {
        Task AddResult(SubmissionRequest submissionRequest);

        Task Migrate();
    }
}
