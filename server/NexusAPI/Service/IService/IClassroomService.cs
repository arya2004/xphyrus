using NexusAPI.Dto;

using NexusAPI.Models;

namespace NexusAPI.Service.IService
{
    public interface IClassroomService
    {
        Task<ResponseDto> Create(HttpContext httpContext, Classroom classroom);
        Task<ResponseDto> Delete(HttpContext httpContext, Guid classroomId);
        Task<ResponseDto> Edit(HttpContext httpContext, Classroom classroom);
        Task<ResponseDto> Get(HttpContext httpContext, Guid classroomId);
        Task<ResponseDto> GetAll(HttpContext httpContext);
        Task<ResponseDto> GetMy(HttpContext httpContext);

    }
}
