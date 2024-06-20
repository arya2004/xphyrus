using AssessmentAPI.Dto;
using AssessmentAPI.Models;
using AssessmentAPI.RabbitMQ;
using AssessmentAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IJudgeService _judgeService;
        private readonly IMQSender _bus;
        private readonly IConfiguration _configuration;

        protected ResponseDto _responseDto;
        public SubmissionController(IJudgeService judgeService, IMQSender bus, IConfiguration configuration)
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


        [HttpPost("Submit")]
        public ActionResult<ResponseDto> Submission([FromBody] CodingAssessmentSubmission submissionDto)
        {


            try
            {

                _bus.SendMessage(submissionDto, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Uploaded";

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

        public ActionResult<bool> BusTest([FromBody] CodingAssessmentSubmission request)
        {
            try
            {
                _bus.SendMessage(request, _configuration.GetValue<string>("TopicAndQueueName:UserSubmissions"));
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
