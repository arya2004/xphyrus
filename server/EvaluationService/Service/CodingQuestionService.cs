using EvaluationService.Data;
using EvaluationService.Service.IService;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace EvaluationService.Service
{
    public class CodingQuestionService : ICodingQuestionService
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public CodingQuestionService(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        // Fetch a CodingQuestion along with its child TestCases based on the CodingQuestionId
        public async Task<CodingQuestion?> GetCodingQuestionByIdAsync(Guid codingQuestionId)
        {
            try
            {
                await using var _db = new ApplicationDbContext(_options);

                // Fetch the CodingQuestion with its TestCases using eager loading
                var codingQuestion = await _db.CodingQuestions
                    .Include(cq => cq.TestCases)
                    .FirstOrDefaultAsync(cq => cq.CodingQuestionId == codingQuestionId);

                return codingQuestion;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

     
    }

}
