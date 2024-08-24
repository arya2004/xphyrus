using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusAPI.Dto;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDashboardController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStudentDashboardService _studentDashboardService;

        public StudentDashboardController(
            IMapper mapper,
            IAuthorizationService authorizationService,
            IStudentDashboardService studentDashboardService)
        {
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _studentDashboardService = studentDashboardService;
        }

        [HttpGet("GetExamsTaken")]
        public async Task<ActionResult<ResponseDto>> GetExamsTaken()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _studentDashboardService.GetExamsTaken(this.HttpContext);
            return _responseDto;
        }

        [HttpGet("GetExamDetails")]
        public async Task<ActionResult<ResponseDto>> GetExamDetails(Guid examId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _studentDashboardService.GetExamDetails(this.HttpContext, examId);
            return _responseDto;
        }
    }
}
