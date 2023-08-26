using Azure;
using Newtonsoft.Json;
using System;
using System.Text;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Service
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
            var client = _httpClientFactory.CreateClient("Judge0");
            var resp = await client.GetAsync($"/submissions/" + response.token.ToString());
            var apiContent = await resp.Content.ReadAsStringAsync();
            var ress = JsonConvert.DeserializeObject<SubmissionStatusResponse>(apiContent);
            return ress;
            return JsonConvert.DeserializeObject<SubmissionStatusResponse>(Convert.ToString(resp));
            
        }

        public async Task<TokenResponse> SubmitPost(SubmissionRequest request)
        {   
            
            return new TokenResponse();
        }
           
    }

}
