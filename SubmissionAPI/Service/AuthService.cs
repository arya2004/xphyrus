using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using SubmissionAPI.Models.Dtos;
using SubmissionAPI.Models.ResReq;
using SubmissionAPI.Service.IService;
using System;
using System.Security.Policy;
using System.Text;


namespace Xphyrus.SubmissionAPI.Service
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

        public async Task<ResponseDto> MarkSubmission(SubmissionDto regStartNotSubDto)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var uri = _configuration.GetValue<string>("ServiceUrls:AuthAPI") + "/api/UserRegistrationAPI/Submit";
            ///api/UserRegistrationAPI/CreateAdminAssesment
            var json = JsonConvert.SerializeObject(regStartNotSubDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto>(responseBody);
        }

        //public async Task<ResponseDto> ToCreateAssesmentAdmin(AssesmentAdminDto assesmentAdminDto)
        //{
        //    //var httpClient = _httpClientFactory.CreateClient();
        //    //var uri = _configuration.GetValue<string>("ServiceUrls:AuthAPI") + "/api/UserRegistrationAPI/CreateAdminAssesment";
        //    /////api/UserRegistrationAPI/CreateAdminAssesment
        //    //var json = JsonConvert.SerializeObject(assesmentAdminDto);
        //    //var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    //var response = await httpClient.PostAsync(uri, content);
        //    //var responseBody = await response.Content.ReadAsStringAsync();
        //    //return JsonConvert.DeserializeObject<ResponseDto>(responseBody);

        //}
        ////will be used to add manually by admin
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
    }

}
