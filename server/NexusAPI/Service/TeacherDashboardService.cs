using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Dto.TeacherDto;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class TeacherDashboardService : ITeacherDashboardService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public TeacherDashboardService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Method to retrieve all students' metadata for a specific test
        public async Task<ResponseDto> GetTestMetadata(Guid testId)
        {
            try
            {
                var testMetadata = await _applicationDbContext.StudentAnswerMetadatas
                    .Include(sam => sam.StudentId)
                    .Include(sam => sam.Test)
                    .Where(sam => sam.Test.TestId == testId)
                    .ToListAsync();

                if (!testMetadata.Any())
                {
                    return new ResponseDto(false, "No metadata found for the specified test.");
                }

                var testMetadataDtos = _mapper.Map<List<StudentAnswerMetadataDto>>(testMetadata);

                return new ResponseDto(testMetadataDtos, true, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, "An error occurred while retrieving the test metadata. Please try again later.");
            }
        }

        // Method to retrieve detailed information for a specific student's exam based on StudentAnswerMetadataId
        public async Task<ResponseDto> GetStudentExamDetails(Guid studentAnswerMetadataId)
        {
            try
            {
                var examDetails = await _applicationDbContext.StudentAnswerMetadatas
                    .Include(sam => sam.Test)
                    .Include(sam => sam.StudentAnswers)
                        .ThenInclude(sa => sa.CodingQuestion)
                    .Include(sam => sam.StudentId)
                    .Where(sam => sam.StudentAnswerMetadataId == studentAnswerMetadataId)
                    .FirstOrDefaultAsync();

                if (examDetails == null)
                {
                    return new ResponseDto(false, "Exam details not found for the specified metadata ID.");
                }

                var examDetailsDto = _mapper.Map<StudentExamDetailsDto>(examDetails);
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
