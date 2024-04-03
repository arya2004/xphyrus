

using EvaluationService.Models;
using EvaluationService.Models.Dtos;

namespace EvaluationService.Service.IService
{
    public interface IJudgeService
    {
        Task<object> SubmitPost(Models.Dtos.SubmissionRequest request);
        Task<SubmissionStatusResponse> GetResponse(TokenResponse response);
    }
}
