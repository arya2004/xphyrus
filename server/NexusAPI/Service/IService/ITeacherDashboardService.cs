using NexusAPI.Dto;

namespace NexusAPI.Service.IService
{
    public interface ITeacherDashboardService
    {
        Task<ResponseDto> GetTestMetadata(Guid testId);
        Task<ResponseDto> GetStudentExamDetails(Guid studentAnswerMetadataId);
    }
}
