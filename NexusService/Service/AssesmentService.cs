using Newtonsoft.Json;
using Xphyrus.NexusService.Models.ResReq;
using Xphyrus.NexusService.Service.IService;

namespace Xphyrus.NexusService.Service
{
    public class AssesmentService : IAssesmentService
    {   
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AssesmentService(IHttpClientFactory httpClientFactory, IConfiguration configuration )
        {

            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<ResponseDto> RegisterForAssesment(string code)
        {
            var client = _httpClientFactory.CreateClient();
            var uri = _configuration.GetValue<string>("ServiceUrls:AssesmentAPI") + "/api/Assesment/CheckIfAssesmentExist/" + code;
            var response = await client.GetAsync(uri);
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            return resp;

        }
    }
}
