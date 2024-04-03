using EvaluationService.Data;
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

        public async Task AddResult(SubmissionRequest submissionRequest)
        {
            try
            {
                EvaluationService.Models.SubmissionRequest model = new EvaluationService.Models.SubmissionRequest()
                {
                    source_code = submissionRequest.source_code

                };
                await using var _db = new ApplicatioDbContext(_options);
                await _db.submissionRequests.AddAsync(model);
                await _db.SaveChangesAsync();   
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
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
