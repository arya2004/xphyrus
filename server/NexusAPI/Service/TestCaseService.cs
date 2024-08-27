using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class TestCaseService : ITestCaseService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseService"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public TestCaseService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<ResponseDto> Create(HttpContext httpContext, TestCase testCase, Guid assessmentId)
        {
            if (testCase == null)
            {
                throw new ArgumentNullException(nameof(testCase));
            }

            try
            {
                var assessment = await _applicationDbContext.CodingQuestions.FirstOrDefaultAsync(a => a.CodingQuestionId == assessmentId);
                if (assessment == null)
                {
                    return new ResponseDto(false, "Assessment not found.");
                }

                testCase.CodingQuestion = assessment;
                await _applicationDbContext.TestCases.AddAsync(testCase);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Test case added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseDto(false, "An error occurred while adding the test case. Please try again later.");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetAllForAssessment(HttpContext httpContext, Guid assessmentId)
        {
            try
            {
                var testCases = await _applicationDbContext.TestCases
                    .Where(tc => tc.CodingQuestion != null && tc.CodingQuestion.CodingQuestionId == assessmentId)
                    .ToListAsync();

                if (testCases == null || testCases.Count == 0)
                {
                    return new ResponseDto(false, $"No test cases found for Assessment: {assessmentId}.");
                }

                return new ResponseDto(testCases, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving test cases.");
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                var testCases = await _applicationDbContext.TestCases.ToListAsync();

                if (testCases == null || testCases.Count == 0)
                {
                    return new ResponseDto(false, "No test cases found.");
                }

                return new ResponseDto(testCases, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving test cases.");
            }
        }


    }
}
