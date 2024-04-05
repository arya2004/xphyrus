using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;

using NexusAPI.Service.IService;
using System.Security.Claims;

namespace NexusAPI.Service
{
    public class NexusService : INexusService
    {

        private readonly ApplicationDbContext _applicationDbContext;
  
        public NexusService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
   
        }

        public async Task<ResponseDto> Create(HttpContext httpContext, Nexus nexus)
        {
            

            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId == null) return new ResponseDto(false, "Invalid Token");

                nexus.Creator = new Guid(userId);

                await _applicationDbContext.Nexus.AddAsync(nexus);
                await _applicationDbContext.SaveChangesAsync();

                return new ResponseDto(true, "Added Successfully");
            }
            catch (Exception ex)
            {

                return new ResponseDto(false, ex.Message.ToString());
            }
        }

        public async Task<ResponseDto> Delete(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                Nexus? company = await _applicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId == nexusId);
                if (company == null)
                {

                    return new ResponseDto(false, "Not Found");
                }
                _applicationDbContext.Nexus.Remove(company);
                await _applicationDbContext.SaveChangesAsync();
                return new ResponseDto(true, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message.ToString());
            }
        }

        public async Task<ResponseDto> Edit(HttpContext httpContext, Nexus nexus)
        {
            try
            {
                Nexus? company = await _applicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId == nexus.NexusId);
                if (company == null)
                {

                    return new ResponseDto(false, "Not Found");
                }
                _applicationDbContext.Nexus.Update(company);
                await _applicationDbContext.SaveChangesAsync();
                return new ResponseDto(true, "Edited Successfully");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message.ToString());
            }
        }

        public async Task<ResponseDto> Get(HttpContext httpContext, Guid nexusId)
        {
            try
            {
                Nexus? company = await _applicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId == nexusId);
                if (company == null)
                {
                    return new ResponseDto(false, "Not Found");
                }
                return new ResponseDto(company,true, "");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message.ToString());
            }
        }

        public async Task<ResponseDto> GetAll(HttpContext httpContext)
        {
            try
            {
                List<Nexus> companies = await _applicationDbContext.Nexus.ToListAsync();
                if (companies == null)
                {
                    return new ResponseDto(false, "Not Found");
                }
                return new ResponseDto(companies, true, "");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message.ToString());
            };
        }

        public async Task<ResponseDto> GetMy(HttpContext httpContext)
        {
            try
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                List<Nexus> companies = await _applicationDbContext.Nexus.Where(_ => _.Creator.ToString() == userId).ToListAsync();
                if (companies == null)
                {
                    return new ResponseDto(false, "Not Found");
                }
                return new ResponseDto(companies, true, "");
            }
            catch (Exception ex)
            {
                return new ResponseDto(false, ex.Message.ToString());
            }
        }

        public async Task<ResponseDto> Join(HttpContext httpContext, string nexusCode)
        {
            try
            {

                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Nexus? companies = await _applicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId.ToString() == nexusCode);
                if (companies == null)
                {
                    return new ResponseDto(false, "Wrong Code");
                }
                var user = await _applicationDbContext.Users.FirstOrDefaultAsync(_ => _.Id.ToString() == userId);
                if (user == null)
                {
                    return new ResponseDto(false, "Invalid Token");
                }
                companies.ApplicationUsers.Add(user);
                await _applicationDbContext.SaveChangesAsync();
                return new ResponseDto(companies, true, "Success");

            }
            catch (Exception ex)
            {

                return new ResponseDto(false, ex.Message.ToString());
            }
        }
    }
}
