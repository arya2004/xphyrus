//using Microsoft.EntityFrameworkCore;
//using NexusAPI.Data;
//using NexusAPI.Dto;
//using NexusAPI.Models;
//using NexusAPI.Service.IService;

//namespace NexusAPI.Service
//{
//    public class TestCaseService : ITestCaseService
//    {
//        private readonly ApplicationDbContext _applicationDbContext;

//        public TestCaseService(ApplicationDbContext applicationDbContext)
//        {
//            _applicationDbContext = applicationDbContext;

//        }

//        public async Task<ResponseDto> Create(HttpContext httpContext, TestCase testCase, Guid assessmentId)
//        {
//            try
//            {
//                CodingAssessment? n = await _applicationDbContext.CodingAssessments.FirstOrDefaultAsync(_ => _.CodingAssessmentId == assessmentId);
//                testCase.CodingAssessment = n;
//                await _applicationDbContext.TestCases.AddAsync(testCase);
//                await _applicationDbContext.SaveChangesAsync();

//                return new ResponseDto(true, "Added Successfully");
//            }
//            catch (Exception ex)
//            {
//                return new ResponseDto(ex.Message, false, "");
//            }
//        }

    

       

//        public async Task<ResponseDto> GetAll(HttpContext httpContext)
//        {
//            try
//            {
//                List<TestCase> testCases = await _applicationDbContext.TestCases.ToListAsync();
//                return new ResponseDto(testCases, true, "");

//            }
//            catch (Exception ex)
//            {
//                return new ResponseDto(ex.Message, false, "");
//            }
//        }

//        public async Task<ResponseDto> GetAllForAssessment(HttpContext httpContext, Guid assessmentId)
//        {
//            try
//            {
//                List<TestCase> testCases= await _applicationDbContext.TestCases.Where(_ => _.CodingAssessment.CodingAssessmentId == assessmentId).ToListAsync();
//                return new ResponseDto(testCases, true, "");

//            }
//            catch (Exception ex)
//            {
//                return new ResponseDto(ex.Message, false, "");
//            }
//        }

      
//    }
//}
