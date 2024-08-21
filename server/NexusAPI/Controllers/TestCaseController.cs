using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class TestCaseController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITestCaseService _testCaseService;

        public TestCaseController(ITestCaseService testCaseService, ApplicationDbContext applicationDbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _testCaseService = testCaseService;
        }

        [HttpGet("GetAll")]
        public ActionResult<ResponseDto> GetAll()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            List<TestCase> testCases = _applicationDbContext.TestCases.ToList();
            _responseDto.Result = testCases;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }

        [HttpGet("GetAllForAssessment")]
        public async Task<ActionResult<ResponseDto>> GetAllForAssessment(Guid assessmentId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _testCaseService.GetAllForAssessment(this.HttpContext, assessmentId);
            return _responseDto;
        }

        [HttpGet("GetOne")]
        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (_responseDto.IsSuccess)
            {
                TestCase? testCase = await _applicationDbContext.TestCases.FirstOrDefaultAsync(_ => _.TestCaseId == new Guid(id));
                _responseDto.Result = testCase;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }

            _responseDto = await _testCaseService.GetAll(this.HttpContext);
            return _responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] TestCaseDto testCaseDto)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            TestCase testCaseToSave = _mapper.Map<TestCase>(testCaseDto);
            _responseDto = await _testCaseService.Create(HttpContext, testCaseToSave, new Guid(testCaseDto.CodingQuestionId));
            return _responseDto;
        }

        [HttpPut]
        public async Task<IActionResult> Edit(TestCase testCase)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return BadRequest(_responseDto);

            try
            {
                _applicationDbContext.TestCases.Update(testCase);
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
                TestCase? testCase = _applicationDbContext.TestCases.FirstOrDefault(_ => _.TestCaseId == id);
                if (testCase == null)
                {
                    _responseDto.Message = "Not Found";
                    _responseDto.IsSuccess = false;
                    return NotFound(_responseDto);
                }
                _applicationDbContext.TestCases.Remove(testCase);
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
