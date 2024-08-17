using NexusAPI.Dto;

namespace NexusAPI.Service.IService
{
    public interface IResultsService
    {
        public Task<ResponseDto> Delete(HttpContext httpContext, Guid codingAssessmentResultId);
        public Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentResultId);
        public Task<ResponseDto> GetAllForAssessment(HttpContext httpContext, Guid nexusId);
        public Task<ResponseDto> GetAll(HttpContext httpContext);
    }
}
