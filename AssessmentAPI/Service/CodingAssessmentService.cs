using AssessmentAPI.Data;
using AssessmentAPI.Dto;
using AssessmentAPI.Models;
using AssessmentAPI.Service.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AssessmentAPI.Service
{
    /// <summary>
    /// Service class for handling operations related to coding assessments.
    /// </summary>
    public class CodingAssessmentService : ICodingAssessmentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingAssessmentService"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        public CodingAssessmentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        /// <summary>
        /// Gets a coding assessment by its identifier.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="codingAssessmentId">The identifier of the coding assessment.</param>
        /// <returns>A <see cref="ResponseDto"/> containing the assessment details or an error message.</returns>
        public async Task<ResponseDto> GetAsync(HttpContext httpContext, Guid codingAssessmentId)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext), "HTTP context cannot be null.");
            }

            if (codingAssessmentId == Guid.Empty)
            {
                return new ResponseDto(false, "Invalid coding assessment ID.");
            }

            try
            {
                CodingAssessment? assessment = await _applicationDbContext.CodingAssessments
                    .FirstOrDefaultAsync(a => a.CodingAssessmentId == codingAssessmentId);

                if (assessment == null)
                {
                    return new ResponseDto(false, "Coding assessment not found.");
                }

                return new ResponseDto(assessment, true, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception details for further analysis (not implemented here)
                // For example: _logger.LogError(ex, "Error occurred while fetching coding assessment.");

                return new ResponseDto(false, "An error occurred while fetching the coding assessment.");
            }
        }
    }
}
