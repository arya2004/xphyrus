using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.MessageBus;
using Xphyrus.SubmissionAPI.Models.Dtos;
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
        public SubmissionAPIController(IJudgeService judgeService, IBus bus, IConfiguration configuration)
        {
            _judgeService = judgeService;
            _bus = bus;
            _configuration = configuration;
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
