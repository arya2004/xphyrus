using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Dto.StudentDto;
using NexusAPI.Models;
using NexusAPI.Service.IService;
using System.Security.Claims;

namespace NexusAPI.Service
{
    public class StudentTestService : IStudentTestService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public StudentTestService(IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<ResponseDto> StartTest(HttpContext httpContext, Guid testId)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new ResponseDto(false, "Invalid Token");
            }

            var student = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (student == null)
            {
                return new ResponseDto(false, "Student not found.");
            }

            var test = await _applicationDbContext.Tests.Include(t => t.CodingQuestions)
                                                        .FirstOrDefaultAsync(t => t.TestId == testId);

            if (test == null)
            {
                return new ResponseDto(false, "Test not found.");
            }

            var studentAnswerMetadata = new StudentAnswerMetadata
            {
                StudentId = student,
                Test = test,
                StartDate = DateTime.UtcNow
            };

            await _applicationDbContext.StudentAnswerMetadatas.AddAsync(studentAnswerMetadata);
            await _applicationDbContext.SaveChangesAsync();

            StudentTestDto studentTestDto = _mapper.Map<StudentTestDto>(test);

            var responseData = new StartTestResponseDto
            {
                Test = studentTestDto,
                StudentAnswerMetadataId = studentAnswerMetadata.StudentAnswerMetadataId
            };

            return new ResponseDto(responseData, true, "Test started successfully.");
        }

        public async Task<ResponseDto> SubmitQuestion(HttpContext httpContext, SubmitQuestionDto submitQuestion, Guid questionId)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new ResponseDto(false, "Invalid Token");
            }

            var student = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (student == null)
            {
                return new ResponseDto(false, "Student not found.");
            }

            var codingQuestion = await _applicationDbContext.CodingQuestions.FirstOrDefaultAsync(cq => cq.CodingQuestionId == questionId);
            if (codingQuestion == null)
            {
                return new ResponseDto(false, "Question not found.");
            }

            var studentAnswerMetadata = await _applicationDbContext.StudentAnswerMetadatas
                .Include(sam => sam.Test)
                .Include(sam => sam.StudentAnswers)
                .FirstOrDefaultAsync(sam => sam.StudentId == student && sam.Test.TestId == submitQuestion.TestId);

            if (studentAnswerMetadata == null)
            {
                return new ResponseDto(false, "Test session not found.");
            }

            var studentAnswer = new StudentAnswer
            {
                SubmittedCode = submitQuestion.Source_code,
                CodingQuestion = codingQuestion,
                Student = student,
                SubmittedDate = DateTime.UtcNow
            };

            studentAnswerMetadata.StudentAnswers.Add(studentAnswer);
            await _applicationDbContext.SaveChangesAsync();

            return new ResponseDto(true, "Question submitted successfully.");
        }


        public async Task<ResponseDto> SubmitTest(HttpContext httpContext, Guid testId)
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return new ResponseDto(false, "Invalid Token");
            }

            var student = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (student == null)
            {
                return new ResponseDto(false, "Student not found.");
            }

            var studentAnswerMetadata = await _applicationDbContext.StudentAnswerMetadatas
                .FirstOrDefaultAsync(sam => sam.StudentId == student && sam.Test.TestId == testId);

            if (studentAnswerMetadata == null)
            {
                return new ResponseDto(false, "Test session not found.");
            }

            studentAnswerMetadata.EndDate = DateTime.UtcNow;
            await _applicationDbContext.SaveChangesAsync();

            return new ResponseDto(true, "Test submitted successfully.");
        }
    }
}
