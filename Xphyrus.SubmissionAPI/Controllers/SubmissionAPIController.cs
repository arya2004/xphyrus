using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xphyrus.MessageBus;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Models.ResReq;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionAPIController : ControllerBase
    {   
        private readonly IJudgeService _judgeService;
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IAssesmentService _assesmentService;
        protected ResponseDto _responseDto;
        public SubmissionAPIController(IJudgeService judgeService, IBus bus, IConfiguration configuration, IAuthService authService, IAssesmentService assesmentService)
        {
            _judgeService = judgeService;
            _bus = bus;
            _configuration = configuration;
            _responseDto = new ResponseDto();
            _authService = authService;
            _assesmentService = assesmentService;
         
        }

        [HttpPut]
       
        public async Task<ActionResult<SubmissionStatusResponse>> GetASubmission(TokenResponse id)
        {
            return await _judgeService.GetResponse(id);
        }
        [HttpPost]

        public async Task<ActionResult<object>> PostSubmission([FromBody]SubmissionRequest request)
        {
            return await _judgeService.SubmitPost(request);
        }
        [Authorize]
        [HttpPost("Submit")]
        public async Task<ActionResult<ResponseDto>> Submission([FromBody] temp t)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                _responseDto = await _assesmentService.MarkSubmission(t.submission);
                if(_responseDto.IsSuccess && _responseDto.Message == "")
                {
                  await _bus.PublishMessage(t.SubmissionRequest, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
                }
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
            
        }

        [HttpPost]
        [Route("BusTest")]

        public async Task<ActionResult<bool>> BusTest([FromBody] SubmissionRequest request)
        {
            try
            {
                await _bus.PublishMessage(request, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }

    
}
