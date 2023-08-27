using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.SubmissionAPI.Models.Dtos;
using Xphyrus.SubmissionAPI.Service.IService;

namespace Xphyrus.SubmissionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionAPIController : ControllerBase
    {   
        private readonly IJudgeService _judgeService;
        public SubmissionAPIController(IJudgeService judgeService)
        {
            _judgeService = judgeService;
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
    }
}
