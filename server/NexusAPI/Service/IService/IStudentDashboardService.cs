using NexusAPI.Dto;

namespace NexusAPI.Service.IService
{
    public interface IStudentDashboardService
    {
        // Method to get all exams taken by the student
        Task<ResponseDto> GetExamsTaken(HttpContext httpContext);

        // Method to get details of a specific exam with all answers
        Task<ResponseDto> GetExamDetails(HttpContext httpContext, Guid examId);

    }
}
