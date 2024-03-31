using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusService.Models.Dto;
using System.Security.Claims;
using Xphyrus.NexusService.Data;
using Xphyrus.NexusService.Models;
using Xphyrus.NexusService.Models.ResReq;

namespace Xphyrus.NexusService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NexusController : ControllerBase
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public NexusController(ApplicationDbContext ApplicationDbContext, IMapper mapper)
        {
            _ApplicationDbContext = ApplicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public ActionResult<ResponseDto> GetAll()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            //if (roles == null || roles.Count == 0 || email == null)
            //{
            //    _responseDto.Message = "invalid token";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            //if (!roles.Contains("ADMIN"))
            //{
            //    _responseDto.Message = "unauthorized";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}

            List<Nexus> companies = _ApplicationDbContext.Nexus.ToList();
            _responseDto.Result = companies;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }

        [HttpGet("GetMy")]
        public ActionResult<ResponseDto> GetMy()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            //if (roles == null || roles.Count == 0 || email == null)
            //{
            //    _responseDto.Message = "invalid token";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            //if (!roles.Contains("ADMIN"))
            //{
            //    _responseDto.Message = "unauthorized";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}

            List<Nexus> companies = _ApplicationDbContext.Nexus.Where(_ => _.Creator.ToString() == userId ).ToList();
            _responseDto.Result = companies;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }

        [HttpPost("Join")]
        public async Task<ActionResult<ResponseDto>> Join(string nexusCode)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            //if (roles == null || roles.Count == 0 || email == null)
            //{
            //    _responseDto.Message = "invalid token";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            //if (!roles.Contains("ADMIN"))
            //{
            //    _responseDto.Message = "unauthorized";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}

            Nexus? companies = await _ApplicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId.ToString() == nexusCode);
            if(companies == null)
            {
                _responseDto.IsSuccess = false;
                return _responseDto;
            }
            var user = await _ApplicationDbContext.Users.FirstOrDefaultAsync(_ => _.Id.ToString() == userId);
            if(user == null)
            {
                _responseDto.IsSuccess = false;
                return _responseDto;
            }
            companies.ApplicationUsers.Add(user);
            await _ApplicationDbContext.SaveChangesAsync();
            _responseDto.Result = companies;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }





        [HttpGet("GetOne")]

        public async Task<ActionResult<ResponseDto>> Get(Guid id)
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            //if (roles == null || roles.Count == 0 || email == null)
            //{
            //    _responseDto.Message = "invalid token";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            //if (!roles.Contains("ADMIN"))
            //{
            //    _responseDto.Message = "unauthorized";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}

            Nexus? companies = await _ApplicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId == id);
            _responseDto.Result = companies;
            _responseDto.IsSuccess = true;
            return Ok(_responseDto);

        }



        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] NexusDto company)
        {

            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (roles == null || roles.Count == 0 || email == null)
            //{
            //    _responseDto.Message = "invalid token";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            //if (!roles.Contains("ADMIN"))
            //{
            //    _responseDto.Message = "unauthorized";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}

            Nexus companyToSave = _mapper.Map<Nexus >(company);
            companyToSave.Creator = new Guid(userId);
            try
            {
                _ApplicationDbContext.Nexus.Add(companyToSave);
                await _ApplicationDbContext.SaveChangesAsync();
                _responseDto.Message = "Added Successfully";
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
                return Ok(_responseDto);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Edit(Nexus company)
        {

            try
            {
                _ApplicationDbContext.Nexus.Update(company);
                await _ApplicationDbContext.SaveChangesAsync();
                _responseDto.Message = "Edited Successfully";
                _responseDto.IsSuccess = true;

            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }

            return Ok(_responseDto);

        }


        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {


            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var roles = HttpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            //if (roles == null || roles.Count == 0 || email == null)
            //{
            //    _responseDto.Message = "invalid token";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            //if (!roles.Contains("ADMIN"))
            //{
            //    _responseDto.Message = "unauthorized";
            //    _responseDto.IsSuccess = false;
            //    return _responseDto;
            //}
            try
            {
                Nexus? company = _ApplicationDbContext.Nexus.FirstOrDefault(_ => _.NexusId == id);
                if (company == null)
                {
                    _responseDto.Message = "NOt Found";
                    _responseDto.IsSuccess = false;
                    return NotFound(_responseDto);
                }
                _ApplicationDbContext.Nexus.Remove(company);
                await _ApplicationDbContext.SaveChangesAsync();
                _responseDto.Message = "Deleted Successfully";
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
                return Ok(_responseDto);
            }

        }
    }
}