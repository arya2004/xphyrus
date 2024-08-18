using NexusAPI.Dto;
using NexusAPI.Dto.TeacherDto;

namespace NexusAPI.Service.IService
{
    public interface IClassroomService
    {
        Task<ResponseDto> GetAsync(HttpContext httpContext, Guid id);
        Task<ResponseDto> CreateAsync(ClassroomDTO classroomDto);
        Task<ResponseDto> UpdateAsync(Guid id, ClassroomDTO classroomDto);
        Task<ResponseDto> DeleteAsync(Guid id);
    }
}
