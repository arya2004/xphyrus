using NexusAPI.Dto;
using NexusAPI.Models;

namespace NexusAPI.Service.IService
{
    public interface ICodingAssessmentService
    {
      
        public Task<ResponseDto> Create(HttpContext httpContext, CodingAssessment codingAssessment, Guid nexusId);

        public Task<ResponseDto> Edit(HttpContext httpContext, CodingAssessment codingAssessment);

        public Task<ResponseDto> Delete(HttpContext httpContext, Guid codingAssessmentId);
        public Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId);
        public Task<ResponseDto> GetAllForNexus(HttpContext httpContext, Guid nexusId);
        public Task<ResponseDto> GetAll(HttpContext httpContext);
    }
}
