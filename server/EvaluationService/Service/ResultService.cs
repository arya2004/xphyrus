using EvaluationService.Data;
using EvaluationService.Dtos;
using EvaluationService.Models;
using EvaluationService.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Xphyrus.EvaluationAPI.Service
{
    public class ResultService : IResultService
    {
        private DbContextOptions<ApplicatioDbContext> _options;
       

        public ResultService(DbContextOptions<ApplicatioDbContext> options)
        {
            _options = options;
        
        }

       

        public async Task AddResult(CodingAssessmentSubmission codingAssessmentSubmission, EvaluationService.Dtos.SubmissionStatusResponse submissionStatusResponse)
        {
            try
            {
                CodingAssessmentResult codingAssessmentResult = new CodingAssessmentResult(codingAssessmentSubmission, submissionStatusResponse);
                
                await using var _db = new ApplicatioDbContext(_options);
                await _db.CodingAssessmentResult.AddAsync(codingAssessmentResult);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }

        public async Task<ResponseDto> GetAllForAssessment(Guid assessmentId)
        {
            try
            {
               
                await using var _applicationDbContext = new ApplicatioDbContext(_options);
                string testCases = "";
                return new ResponseDto(testCases, true, "");

            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false, "");
            }
        }

        public async Task Migrate()
        {
            try
            {
                await using var _db = new ApplicatioDbContext(_options);
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    await _db.Database.MigrateAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
