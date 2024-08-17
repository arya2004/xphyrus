using NexusAPI.Dto;
using NexusAPI.Models;

namespace NexusAPI.Service.IService
{
    public interface ICodingAssessmentService
    {

        /// <summary>
        /// Retrieves a coding assessment by its identifier.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="codingAssessmentId">The identifier of the coding assessment.</param>
        /// <returns>A <see cref="ResponseDto"/> containing the assessment details or an error message.</returns>
        Task<ResponseDto> GetAsync(HttpContext httpContext, Guid codingAssessmentId);
        public Task<ResponseDto> Create(HttpContext httpContext, CodingAssessment codingAssessment, Guid nexusId);

        public Task<ResponseDto> Edit(HttpContext httpContext, CodingAssessment codingAssessment);

        public Task<ResponseDto> Delete(HttpContext httpContext, Guid codingAssessmentId);
        public Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId);
        public Task<ResponseDto> GetAllForNexus(HttpContext httpContext, Guid nexusId);
        public Task<ResponseDto> GetAll(HttpContext httpContext);
    }
}
