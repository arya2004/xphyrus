using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class ResultsService : IResultsService
    {
        private readonly ResultsDbContext _applicationDbContext;

        public ResultsService(ResultsDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }



        public Task<ResponseDto> Delete(HttpContext httpContext, Guid codingAssessmentId)
        {
            throw new NotImplementedException();
        }

      

        public async Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId)
        {
            try
            {
                CodingAssessmentResult? assessments = await _applicationDbContext.CodingAssessmentResult.FirstOrDefaultAsync(_ => _.CodingAssessmentResultId== codingAssessmentId);
                if (assessments == null)
                {
                    return new ResponseDto("not found", false, "");
                }
                
                return new ResponseDto(assessments, true, "");

            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false, "");
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                List<CodingAssessmentResult> assessments = await _applicationDbContext.CodingAssessmentResult.ToListAsync();
                return new ResponseDto(assessments, true, "");

            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false, "");
            }
        }

        public async Task<ResponseDto> GetAllForAssessment(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                List<CodingAssessmentResult> assessments = await _applicationDbContext.CodingAssessmentResult.Where(_ => _.AssessmentId == nexusId).ToListAsync();
                return new ResponseDto(assessments, true, "");

            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false, "");
            }
        }
    }
}
