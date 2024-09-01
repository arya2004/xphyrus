using EvaluationService.Data;
using EvaluationService.Service.IService;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace EvaluationService.Service
{
    public class StudentResultService : IStudentResultService
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public StudentResultService(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        // Adds a StudentAnswer to the existing StudentAnswerMetadata
        public async Task AddStudentAnswerAsync(Guid studentAnswerMetadataId, StudentAnswer studentAnswer)
        {
            try
            {
                await using var _db = new ApplicationDbContext(_options);

                // Retrieve the existing StudentAnswerMetadata
                var studentAnswerMetadata = await _db.StudentAnswerMetadatas
                    .Include(sam => sam.StudentAnswers)
                    .FirstOrDefaultAsync(sam => sam.StudentAnswerMetadataId == studentAnswerMetadataId);

                if (studentAnswerMetadata == null)
                {
                    throw new Exception("StudentAnswerMetadata not found.");
                }

                // Add the new StudentAnswer to the StudentAnswerMetadata
                studentAnswerMetadata.StudentAnswers.Add(studentAnswer);

                // Save changes to the database
                _db.StudentAnswerMetadatas.Update(studentAnswerMetadata);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }

}
