using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Models.ResReq;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Service
{
    public class AssesmentService : IAssesmentService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AssesmentService(IHttpClientFactory httpCLientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpCLientFactory;

            _configuration = configuration;

        }
        public async Task<ResponseDto> MarkSubmission(SubmissionDto regStartNotSubDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            //https://localhost:7000/api/Participants/Submit
            var uri = _configuration.GetValue<string>("ServiceUrls:AssesmentAPI") + "/api/Participants/Submit";
            ///api/UserRegistrationAPI/CreateAdminAssesment
            var json = JsonConvert.SerializeObject(regStartNotSubDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto>(responseBody);
        }
    }
}
