using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xphyrus.EvaluationAPI.Data;
using Xphyrus.EvaluationAPI.Factory;
using Xphyrus.EvaluationAPI.Models;
using Xphyrus.EvaluationAPI.Models.Dtos;
using Xphyrus.EvaluationAPI.Service.IService;

namespace Xphyrus.EvaluationAPI.Service
{
    public class ResultService : IResultService
    {
        private DbContextOptions<ApplicatioDbContext> _options;
       

        public ResultService(DbContextOptions<ApplicatioDbContext> options)
        {
            _options = options;
        
        }

        public async Task AddResult(Models.Dtos.SubmissionRequest submissionRequest)
        {
            try
            {
                Models.SubmissionRequest model = new Models.SubmissionRequest()
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

       
    }
}
