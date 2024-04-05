

using EvaluationService.Dtos;
using EvaluationService.Models;

namespace EvaluationService.Service.IService
{
    public interface IJudgeService
    {
        Task<TokenResponse> SubmitPost(JudgeRequest request);
        Task<SubmissionStatusResponse> GetResponse(TokenResponse response);
    }
}
