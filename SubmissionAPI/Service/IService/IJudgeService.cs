
using SubmissionAPI.Models.Dtos;

namespace SubmissionAPI.Service.IService
{
    public interface IJudgeService
    {
        Task<TokenResponse> SubmitPost(SubmissionRequest request);
        Task<SubmissionStatusResponse> GetResponse(TokenResponse response);
    }
}
