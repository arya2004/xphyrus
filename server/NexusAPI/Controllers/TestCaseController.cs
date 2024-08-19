//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NexusAPI.Data;
//using NexusAPI.Dto;
//using NexusAPI.Models;
//using NexusAPI.Service.IService;

//namespace NexusAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TestCaseController : ControllerBase
//    {
//        private readonly ApplicationDbContext _ApplicationDbContext;
//        private ResponseDto _responseDto;
//        private IMapper _mapper;
//        private readonly IAuthorizationService _authorizationService;
//        private readonly ITestCaseService _codingAssessmentService;
//        public TestCaseController(ITestCaseService codingAssessment, ApplicationDbContext ApplicationDbContext, IMapper mapper, IAuthorizationService authorizationService)
//        {
//            _ApplicationDbContext = ApplicationDbContext;
//            _responseDto = new ResponseDto();
//            _mapper = mapper;
//            _authorizationService = authorizationService;
//            _codingAssessmentService = codingAssessment;
//        }
//        [HttpGet("GetAll")]
//        public ActionResult<ResponseDto> GetAll()
//        {

//            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
//            if (!_responseDto.IsSuccess) return _responseDto;

//            List<TestCase> companies = _ApplicationDbContext.TestCases.ToList();
//            _responseDto.Result = companies;
//            _responseDto.IsSuccess = true;
//            return _responseDto;
//        }

//        [HttpGet("GetAllForAssessment")]
//        public async Task<ActionResult<ResponseDto>> GetAllForAssessment(string assessmentId)
//        {
//            //_responseDto = _authorizationService.VerifyToken(this.HttpContext);
//            //if (!_responseDto.IsSuccess) return _responseDto;

//            _responseDto = await _codingAssessmentService.GetAllForAssessment(HttpContext, new Guid(assessmentId));
//            return _responseDto;
//        }







//        [HttpGet("GetOne")]

//        public async Task<ActionResult<ResponseDto>> Get(string id)
//        {
//            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
//            if (!_responseDto.IsSuccess) return _responseDto;

//            TestCase? assessment = await _ApplicationDbContext.TestCases.FirstOrDefaultAsync(_ => _.TestCaseId == new Guid(id));
//            _responseDto.Result = assessment;
//            _responseDto.IsSuccess = true;
//            return Ok(_responseDto);

//        }



//        [HttpPost]
//        public async Task<ActionResult<ResponseDto>> Create([FromBody] CreateTestCase company)
//        {

//            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
//            if (!_responseDto.IsSuccess) return _responseDto;


//            TestCase companyToSave = _mapper.Map<TestCase>(company);
//            _responseDto = await _codingAssessmentService.Create(HttpContext, companyToSave, company.AssociatedCodingAssessment);
//            return _responseDto;

//        }



//        [HttpDelete]
//        [ActionName("Delete")]
//        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
//        {

//            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
//            if (!_responseDto.IsSuccess) return _responseDto;


//            try
//            {
//                Classroom? company = _ApplicationDbContext.Classrooms.FirstOrDefault(_ => _.ClassroomId == id);
//                if (company == null)
//                {
//                    _responseDto.Message = "NOt Found";
//                    _responseDto.IsSuccess = false;
//                    return NotFound(_responseDto);
//                }
//                _ApplicationDbContext.Classrooms.Remove(company);
//                await _ApplicationDbContext.SaveChangesAsync();
//                _responseDto.Message = "Deleted Successfully";
//                _responseDto.IsSuccess = true;
//                return Ok(_responseDto);
//            }
//            catch (Exception ex)
//            {

//                _responseDto.Message = ex.Message;
//                _responseDto.IsSuccess = false;
//                return Ok(_responseDto);
//            }

//        }
//    }
//}
