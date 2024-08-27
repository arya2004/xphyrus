using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITestService _testService;

        public TestController(ITestService testService, ApplicationDbContext applicationDbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _testService = testService;
        }

        [HttpGet("GetAll")]
        public ActionResult<ResponseDto> GetAll()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            List<Test> tests = _applicationDbContext.Tests.ToList();
            _responseDto.Result = tests;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }

        [HttpGet("GetAllForClassroom")]
        public async Task<ActionResult<ResponseDto>> GetAllForClassroom(string classroomId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _testService.GetAllForClassroom(HttpContext, new Guid(classroomId));
            return _responseDto;
        }

        [HttpGet("GetOne")]
        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (_responseDto.IsSuccess)
            {
                Test? test = await _applicationDbContext.Tests.FirstOrDefaultAsync(t => t.TestId == new Guid(id));
                _responseDto.Result = test;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }

            _responseDto = await _testService.GetAsync(HttpContext, new Guid(id));
            return _responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] TestDto testDto)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            Test testToSave = _mapper.Map<Test>(testDto);
            _responseDto = await _testService.Create(HttpContext, testToSave, new Guid(testDto.ClassroomId));
            return _responseDto;
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Test test)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return BadRequest(_responseDto);

            try
            {
                _applicationDbContext.Tests.Update(test);
                await _applicationDbContext.SaveChangesAsync();
                _responseDto.Message = "Edited Successfully";
                _responseDto.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }

            return Ok(_responseDto);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            try
            {
                Test? test = await _applicationDbContext.Tests.FirstOrDefaultAsync(t => t.TestId == id);
                if (test == null)
                {
                    _responseDto.Message = "Not Found";
                    _responseDto.IsSuccess = false;
                    return NotFound(_responseDto);
                }
                _applicationDbContext.Tests.Remove(test);
                await _applicationDbContext.SaveChangesAsync();
                _responseDto.Message = "Deleted Successfully";
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
                return Ok(_responseDto);
            }
        }
    }
}
