using NexusAPI.Dto;
using NexusAPI.Dto.StudentDto;

namespace NexusAPI.Service.IService
{
    public interface IStudentTestService
    {
        public Task<ResponseDto> StartTest(HttpContext httpContext, Guid testId);
        public Task<ResponseDto> SubmitQuestion(HttpContext httpContext, SubmitQuestionDto submitQuestion, Guid questionId);
        public Task<ResponseDto> SubmitTest(HttpContext httpContext, Guid testId);

    }
}
