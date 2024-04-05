using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Service
{
    public class CodingAssessmentService : ICodingAssessmentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CodingAssessmentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        public async Task<ResponseDto> Create(HttpContext httpContext, CodingAssessment codingAssessment, Guid nexusId)
        {
            try
            {
                Nexus? n = await _applicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId == nexusId);
                codingAssessment.Nexus = n;
                await _applicationDbContext.CodingAssessments.AddAsync(codingAssessment);
                await _applicationDbContext.SaveChangesAsync();
   
                return new ResponseDto(true, "Added Successfully");
            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false, "");
            }
        }

        public Task<ResponseDto> Delete(HttpContext httpContext, Guid codingAssessmentId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Edit(HttpContext httpContext, CodingAssessment codingAssessment)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> GetAllForNexus(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                List<CodingAssessment> assessments = await _applicationDbContext.CodingAssessments.Where(_ => _.Nexus.NexusId == nexusId).ToListAsync();
                return new ResponseDto(assessments, true, "");

            }
            catch (Exception ex)
            {
                return new ResponseDto(ex.Message, false, "");
            }
        }
    }
}
