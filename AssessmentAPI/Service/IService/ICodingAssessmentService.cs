using AssessmentAPI.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AssessmentAPI.Service.IService
{
    /// <summary>
    /// Interface for the Coding Assessment service.
    /// </summary>
    public interface ICodingAssessmentService
    {
        /// <summary>
        /// Retrieves a coding assessment by its identifier.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="codingAssessmentId">The identifier of the coding assessment.</param>
        /// <returns>A <see cref="ResponseDto"/> containing the assessment details or an error message.</returns>
        Task<ResponseDto> GetAsync(HttpContext httpContext, Guid codingAssessmentId);
    }
}
