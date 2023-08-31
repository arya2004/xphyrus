using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xphyrus.EvaluationAPI.Data;
using Xphyrus.EvaluationAPI.Factory;
using Xphyrus.EvaluationAPI.Models.Dtos;
using Xphyrus.EvaluationAPI.Service.IService;

namespace Xphyrus.EvaluationAPI.Service
{
    public class ResultService : IResultService
    {
        private DbContextOptions<ApplicatioDbContext> _options;
        private readonly IJudgeServiceFactory _judgeService;

        public ResultService(DbContextOptions<ApplicatioDbContext> options, IJudgeServiceFactory judgeService)
        {
            _options = options;
            _judgeService = judgeService;
        }

        public Task AddResult(SubmissionRequest submissionRequest)
        {
            throw new NotImplementedException();
        }

        private async Task<ActionResult<SubmissionStatusResponse>> GetASubmission(TokenResponse id)
        {
            return await _judgeService.GetResponse(id);
        }

        private async Task<ActionResult<object>> PostSubmission(SubmissionRequest request)
        {
            return await _judgeService.SubmitPost(request);
        }
    }
}
