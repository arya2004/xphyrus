using EvaluationService.Models.Dtos;
using EvaluationService.Models.ResReq;
using EvaluationService.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;


namespace EvaluationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationAPIController : ControllerBase
    {
        private readonly IJudgeService _judgeService;
        protected ResponseDto _responseDto;
        public EvaluationAPIController(IJudgeService judgeService)
        {
            _judgeService = judgeService;
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




    }
}
