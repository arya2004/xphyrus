using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusAPI.Dto;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherDashboardController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITeacherDashboardService _teacherDashboardService;

        public TeacherDashboardController(IAuthorizationService authorizationService, ITeacherDashboardService teacherDashboardService)
        {
            _responseDto = new ResponseDto();
    
            _authorizationService = authorizationService;
            _teacherDashboardService = teacherDashboardService;
        }

        [HttpGet("GetTestMetadata")]
        public async Task<ActionResult<ResponseDto>> GetTestMetadata(Guid testId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _teacherDashboardService.GetTestMetadata(testId);
            return _responseDto;
        }

        [HttpGet("GetStudentExamDetails")]
        public async Task<ActionResult<ResponseDto>> GetStudentExamDetails(Guid studentAnswerMetadataId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _teacherDashboardService.GetStudentExamDetails(studentAnswerMetadataId);
            return _responseDto;
        }
    }
}
