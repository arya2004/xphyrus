using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusAPI.Dto;
using NexusAPI.Dto.StudentDto;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTestController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStudentTestService _studentTestService;

        public StudentTestController(IMapper mapper, IAuthorizationService authorizationService, IStudentTestService studentTestService)
        {
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _studentTestService = studentTestService;
        }

        [HttpPost("StartTest")]
        public async Task<ActionResult<ResponseDto>> StartTest(Guid testId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _studentTestService.StartTest(this.HttpContext, testId);
            return _responseDto;
        }

        [HttpPost("SubmitQuestion")]
        public async Task<ActionResult<ResponseDto>> SubmitQuestion([FromBody] SubmitQuestionDto submitQuestion, Guid questionId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _studentTestService.SubmitQuestion(this.HttpContext, submitQuestion, questionId);
            return _responseDto;
        }

        [HttpPost("SubmitTest")]
        public async Task<ActionResult<ResponseDto>> SubmitTest(Guid testId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _studentTestService.SubmitTest(this.HttpContext, testId);
            return _responseDto;
        }
    }
}
