using AssessmentAPI.Data;
using AssessmentAPI.Dto;
using AssessmentAPI.Models;
using AssessmentAPI.Service.IService;
using Microsoft.EntityFrameworkCore;


namespace NexusAPI.Service
{
    public class CodingAssessmentService : ICodingAssessmentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CodingAssessmentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

     

        public async Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId)
        {
            try
            {
                CodingAssessment? assessment = await _applicationDbContext.CodingAssessments.FirstOrDefaultAsync(_ => _.CodingAssessmentId == codingAssessmentId);
                if (assessment == null)
                {
                    return new ResponseDto(false, "Not Found");
                }
                return new ResponseDto(assessment, true, "");
            }
            catch (Exception ex)
            {

                return new ResponseDto(false, ex.Message);
            }

        }


    }
}
