using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexusAPI.Service
{
    public class CodingAssessmentService : ICodingAssessmentService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CodingAssessmentService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        /// <summary>
        /// Creates a new CodingAssessment and associates it with a specified Nexus.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <param name="codingAssessment">The CodingAssessment to create.</param>
        /// <param name="nexusId">The ID of the Nexus to associate with.</param>
        /// <returns>A ResponseDto indicating success or failure.</returns>
        public async Task<ResponseDto> Create(HttpContext httpContext, CodingAssessment codingAssessment, Guid nexusId)
        {
            if (codingAssessment == null)
            {
                throw new ArgumentNullException(nameof(codingAssessment));
            }

            try
            {
                var nexus = await _applicationDbContext.Nexus.FirstOrDefaultAsync(n => n.NexusId == nexusId);
                if (nexus == null)
                {
                    return new ResponseDto(false, "Nexus not found.");
                }

                codingAssessment.Nexus = nexus;
                await _applicationDbContext.CodingAssessments.AddAsync(codingAssessment);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "CodingAssessment added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(dbEx, "An error occurred while adding the coding assessment.");
                return new ResponseDto(false, "An error occurred while adding the coding assessment. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An unexpected error occurred.");
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public Task<ResponseDto> Delete(HttpContext httpContext, Guid codingAssessmentId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Edit(HttpContext httpContext, CodingAssessment codingAssessment)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Get(HttpContext httpContext, Guid codingAssessmentId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all CodingAssessments associated with a specified Nexus.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <param name="nexusId">The ID of the Nexus.</param>
        /// <returns>A ResponseDto containing a list of CodingAssessments.</returns>
        public async Task<ResponseDto> GetAllForNexus(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                var assessments = await _applicationDbContext.CodingAssessments
                    .Where(ca => ca.Nexus.NexusId == nexusId)
                    .ToListAsync();

                if (assessments == null || assessments.Count == 0)
                {
                    return new ResponseDto(false, "No coding assessments found for the specified Nexus.");
                }

                return new ResponseDto(assessments, true, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An error occurred while retrieving coding assessments.");
                return new ResponseDto(false, "An error occurred while retrieving coding assessments. Please try again later.");
            }
        }
    }
}
