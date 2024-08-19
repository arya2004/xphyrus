using NexusAPI.Dto;
using NexusAPI.Models;

namespace NexusAPI.Service.IService
{
    public interface ITestService
    {
        Task<ResponseDto> GetAsync(HttpContext httpContext, Guid testId);
        Task<ResponseDto> Create(HttpContext httpContext, Test test, Guid classroomId);
        Task<ResponseDto> Delete(HttpContext httpContext, Guid testId);
        Task<ResponseDto> Edit(HttpContext httpContext, Test test);
        Task<ResponseDto> GetAll(HttpContext httpContext);
        Task<ResponseDto> GetAllForClassroom(HttpContext httpContext, Guid classroomId);
    }
}
