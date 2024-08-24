using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Dto.StudentDto;
using NexusAPI.Service.IService;
using System.Security.Claims;

namespace NexusAPI.Service
{
    public class StudentDashboardService : IStudentDashboardService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public StudentDashboardService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Method to retrieve all exams taken by the student
        public async Task<ResponseDto> GetExamsTaken(HttpContext httpContext)
        {
            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                var studentExams = await _applicationDbContext.StudentAnswerMetadatas
                    .Include(sam => sam.Test)
                    .Where(sam => sam.StudentId.Id == userId)
                    .ToListAsync();

                var examOverviews = _mapper.Map<List<ExamOverviewDto>>(studentExams);

                return new ResponseDto(examOverviews, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving the exams. Please try again later.");
            }
        }

        // Method to retrieve details of a specific exam with all answers
        public async Task<ResponseDto> GetExamDetails(HttpContext httpContext, Guid examId)
        {
            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                var examDetails = await _applicationDbContext.StudentAnswerMetadatas
                    .Include(sam => sam.Test)
                    .Include(sam => sam.StudentAnswers)
                        .ThenInclude(sa => sa.CodingQuestion)
                    .Where(sam => sam.StudentId.Id == userId && sam.StudentAnswerMetadataId == examId)
                    .FirstOrDefaultAsync();

                if (examDetails == null)
                {
                    return new ResponseDto(false, "Exam not found or not taken by the student.");
                }

                var examDetailsDto = _mapper.Map<ExamDetailsDto>(examDetails);
                var testDto = _mapper.Map<TestDto>(examDetails.Test);

                var result = new
                {
                    ExamDetails = examDetailsDto,
                    Test = testDto
                };

                return new ResponseDto(result, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving the exam details. Please try again later.");
            }
        }
    }
}