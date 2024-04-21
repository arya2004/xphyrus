using NexusAPI.Dto;
using NexusAPI.Models;

namespace NexusAPI.Service.IService
{
    public interface ITestCaseService
    {
        public Task<ResponseDto> Create(HttpContext httpContext, TestCase testCase, Guid assessmentId);

    
        public Task<ResponseDto> GetAllForAssessment(HttpContext httpContext, Guid assessmentId);
        public Task<ResponseDto> GetAll(HttpContext httpContext);
    }
}
