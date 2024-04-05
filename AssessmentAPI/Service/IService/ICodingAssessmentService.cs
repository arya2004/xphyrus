using AssessmentAPI.Dto;


namespace AssessmentAPI.Service.IService
{
    public interface ICodingAssessmentService
    {


        public Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId);

    }
}
