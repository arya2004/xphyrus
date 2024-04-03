using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubmissionAPI.Models.Dtos;
using SubmissionAPI.Models.ResReq;
using SubmissionAPI.Service.IService;
using System.Security.Claims;
using Xphyrus.MessageBus;


namespace SubmissionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRunController : ControllerBase
    {
        private readonly IJudgeService _judgeService;
    
        private readonly IConfiguration _configuration;
 
        protected ResponseDto _responseDto;
        public TestRunController(IJudgeService judgeService,  IConfiguration configuration)
        {
            _judgeService = judgeService;
           
            _configuration = configuration;
            _responseDto = new ResponseDto();
       

        }

        [HttpPost("Run")]
        public async Task<ActionResult<ResponseDto>> RunCode([FromBody] TestRunDto testRunDto)
        {
            //var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            //var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //if(email == null || id == null)
            //{   
            //    _responseDto.IsSuccess = false;
            //    _responseDto.Message = "invalid token";
            //    return _responseDto;
            //}

            SubmissionRequest submissionRequest = new SubmissionRequest(testRunDto);
            TokenResponse res = await _judgeService.SubmitPost(submissionRequest);
            Thread.Sleep(1000);
            SubmissionStatusResponse statusResponse = await _judgeService.GetResponse(res);

            _responseDto.Result = statusResponse;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }
    }
}
