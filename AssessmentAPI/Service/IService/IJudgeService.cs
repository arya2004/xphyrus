using AssessmentAPI.Dto;

namespace AssessmentAPI.Service.IService
{
    public interface IJudgeService
    {
        Task<TokenResponse> SubmitPost(SubmissionRequest request);
        Task<SubmissionStatusResponse> GetResponse(TokenResponse response);
    }
}
