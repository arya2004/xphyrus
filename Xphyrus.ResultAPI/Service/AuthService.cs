using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System;
using System.Security.Policy;
using System.Text;

using Xphyrus.ResultAPI.Models.ResReq;
using Xphyrus.ResultAPI.Service.IService;

namespace Xphyrus.ResultAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }



        public Task<ResponseDto> ToCreateAssesmentAdmin()
        {
            throw new NotImplementedException();
        }


        public Task<ResponseDto> ToCreateAssesmentParticipant()
        {
            throw new NotImplementedException();
        }




        public Task<ResponseDto> ToStartAssesment()
        {
            throw new NotImplementedException();
        }

        //public async Task<ResponseDto> ToCreateAssesmentAdmin(AssesmentAdminDto assesmentAdminDto)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var uri = _configuration.GetValue<string>("ServiceUrls:AuthAPI") + "/api/UserRegistrationAPI/CreateAdminAssesment";
        //    ///api/UserRegistrationAPI/CreateAdminAssesment
        //    var json = JsonConvert.SerializeObject(assesmentAdminDto);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(uri, content);
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ResponseDto>(responseBody);

        //}
        //will be used to add manually by admin
        //public async Task<ResponseDto> ToCreateAssesmentParticipant(AssesmentParticipantDto assesmentParticipant)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var uri = _configuration.GetValue<string>("ServiceUrls:AuthAPI") + "/api/UserRegistrationAPI/CreateParticipantAssesment";
        //    var json = JsonConvert.SerializeObject(assesmentParticipant);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(uri, content);
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ResponseDto>(responseBody);

        //}

        //public async Task<ResponseDto> ToStartAssesment(StartAssesmentDto startAssesmentDto)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var uri = _configuration.GetValue<string>("ServiceUrls:AuthAPI") + "/api/UserRegistrationAPI/Start";
        //    var json = JsonConvert.SerializeObject(startAssesmentDto);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await httpClient.PostAsync(uri, content);
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ResponseDto>(responseBody);
        //}
    }

}