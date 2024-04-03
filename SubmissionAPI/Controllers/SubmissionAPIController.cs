using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubmissionAPI.Models.Dtos;
using SubmissionAPI.Models.ResReq;
using SubmissionAPI.RabbitMQ;
using SubmissionAPI.Service.IService;
using System.Security.Claims;
using Xphyrus.MessageBus;

namespace SubmissionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionAPIController : ControllerBase
    {
        private readonly IJudgeService _judgeService;
        private readonly IMQSender _bus;
        private readonly IConfiguration _configuration;
    
        protected ResponseDto _responseDto;
        public SubmissionAPIController(IJudgeService judgeService, IMQSender bus, IConfiguration configuration)
        {
            _judgeService = judgeService;
            _bus = bus;
            _configuration = configuration;
            _responseDto = new ResponseDto();
          

        }

        [HttpPut]

        public async Task<ActionResult<SubmissionStatusResponse>> GetASubmission(TokenResponse id)
        {
            return await _judgeService.GetResponse(id);
        }
        [HttpPost]

        public async Task<ActionResult<object>> PostSubmission([FromBody] SubmissionRequest request)
        {
            return await _judgeService.SubmitPost(request);
        }
        [Authorize]
        [HttpPost("Submit")]
        public async Task<ActionResult<ResponseDto>> Submission([FromBody] SubmissionDto submissionDto)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // t.SubmissionRequest.StudentId = Int32.Parse(id);
            //  t.SubmissionRequest.AssesmentId = Int32.Parse(t.submission.AssignmentId);

            try
            {
             
                    _bus.SendMessage(submissionDto.source_code, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
              
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
                _bus.SendMessage(request.source_code, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
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
