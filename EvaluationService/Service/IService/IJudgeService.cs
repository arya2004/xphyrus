

using EvaluationService.Models;
using EvaluationService.Models.Dtos;

namespace EvaluationService.Service.IService
{
    public interface IJudgeService
    {
        Task<TokenResponse> SubmitPost(JudgeRequest request);
        Task<SubmissionStatusResponse> GetResponse(TokenResponse response);
    }
}
