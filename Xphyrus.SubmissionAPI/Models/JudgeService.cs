using Newtonsoft.Json;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Models
{
    public class JudgeService : IJudgeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public JudgeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<SubmissionStatusResponse> GetResponse(TokenResponse response)
        {
            return new SubmissionStatusResponse();
        }

        public async Task<TokenResponse> SubmitPost(SubmissionRequest request)
        {
           return new TokenResponse();
        }
    }
}
