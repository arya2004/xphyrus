
using Xphyrus.EvaluationAPI.Models.Dtos;

namespace Xphyrus.EvaluationAPI.Service.IService
{
    public interface IJudgeService
    {
        Task<object> SubmitPost(SubmissionRequest request);
        Task<SubmissionStatusResponse> GetResponse(TokenResponse response);
    }
}
