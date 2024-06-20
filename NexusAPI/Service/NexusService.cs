using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NexusAPI.Service
{
    public class NexusService : INexusService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NexusService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        /// <summary>
        /// Creates a new Nexus entity.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <param name="nexus">The Nexus entity to create.</param>
        /// <returns>A ResponseDto indicating success or failure.</returns>
        public async Task<ResponseDto> Create(HttpContext httpContext, Nexus nexus)
        {
            if (nexus == null)
            {
                throw new ArgumentNullException(nameof(nexus));
            }

            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                nexus.Creator = new Guid(userId);

                await _applicationDbContext.Nexus.AddAsync(nexus);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Nexus added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(dbEx, "An error occurred while adding the nexus.");
                return new ResponseDto(false, "An error occurred while adding the Nexus. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An unexpected error occurred.");
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Delete(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                var nexus = await _applicationDbContext.Nexus.FirstOrDefaultAsync(n => n.NexusId == nexusId);
                if (nexus == null)
                {
                    return new ResponseDto(false, "Nexus not found.");
                }

                _applicationDbContext.Nexus.Remove(nexus);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Nexus deleted successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(dbEx, "An error occurred while deleting the nexus.");
                return new ResponseDto(false, "An error occurred while deleting the Nexus. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An unexpected error occurred.");
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Edit(HttpContext httpContext, Nexus nexus)
        {
            if (nexus == null)
            {
                throw new ArgumentNullException(nameof(nexus));
            }

            try
            {
                var existingNexus = await _applicationDbContext.Nexus.FirstOrDefaultAsync(n => n.NexusId == nexus.NexusId);
                if (existingNexus == null)
                {
                    return new ResponseDto(false, "Nexus not found.");
                }

                _applicationDbContext.Entry(existingNexus).CurrentValues.SetValues(nexus);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Nexus edited successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(dbEx, "An error occurred while editing the nexus.");
                return new ResponseDto(false, "An error occurred while editing the Nexus. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An unexpected error occurred.");
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<ResponseDto> Get(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                var nexus = await _applicationDbContext.Nexus.FirstOrDefaultAsync(n => n.NexusId == nexusId);
                if (nexus == null)
                {
                    return new ResponseDto(false, "Nexus not found.");
                }

                return new ResponseDto(nexus, true, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An error occurred while retrieving the nexus.");
                return new ResponseDto(false, "An error occurred while retrieving the Nexus. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                var nexuses = await _applicationDbContext.Nexus.ToListAsync();
                if (nexuses == null || nexuses.Count == 0)
                {
                    return new ResponseDto(false, "No nexuses found.");
                }

                return new ResponseDto(nexuses, true, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An error occurred while retrieving nexuses.");
                return new ResponseDto(false, "An error occurred while retrieving nexuses. Please try again later.");
            }
        }

        public async Task<ResponseDto> GetMy(HttpContext httpContext)
        {
            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                var nexuses = await _applicationDbContext.Nexus.Where(n => n.Creator.ToString() == userId).ToListAsync();
                if (nexuses == null || nexuses.Count == 0)
                {
                    return new ResponseDto(false, "No nexuses found for the user.");
                }

                return new ResponseDto(nexuses, true, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An error occurred while retrieving user's nexuses.");
                return new ResponseDto(false, "An error occurred while retrieving user's nexuses. Please try again later.");
            }
        }

        public async Task<ResponseDto> Join(HttpContext httpContext, string nexusCode)
        {
            if (string.IsNullOrEmpty(nexusCode))
            {
                return new ResponseDto(false, "Nexus code cannot be empty.");
            }

            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                var nexus = await _applicationDbContext.Nexus.FirstOrDefaultAsync(n => n.NexusId.ToString() == nexusCode);
                if (nexus == null)
                {
                    return new ResponseDto(false, "Invalid Nexus code.");
                }

                var user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
                if (user == null)
                {
                    return new ResponseDto(false, "Invalid Token");
                }

                nexus.ApplicationUsers.Add(user);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(nexus, true, "Successfully joined Nexus.");
            }
            catch (DbUpdateException dbEx)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(dbEx, "An error occurred while joining the nexus.");
                return new ResponseDto(false, "An error occurred while joining the Nexus. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log the error (uncomment the line below after configuring logging)
                // _logger.LogError(ex, "An unexpected error occurred.");
                return new ResponseDto(false, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
