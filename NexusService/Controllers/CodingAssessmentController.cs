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
    public class CodingAssessmentController : ControllerBase
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;

        public CodingAssessmentController(ApplicationDbContext ApplicationDbContext, IMapper mapper)
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

        [HttpGet("GetAllForNexus")]
        public ActionResult<ResponseDto> GetAllForNexus(string NexusId)
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

            Guid nId = new Guid(NexusId);

            List<CodingAssessment> assessments= _ApplicationDbContext.CodingAssessments.Where(_ => _.Nexus.NexusId == nId).ToList();
            _responseDto.Result = assessments;
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

            CodingAssessment? assessment= await _ApplicationDbContext.CodingAssessments.FirstOrDefaultAsync(_ => _.CodingAssessmentId== id);
            _responseDto.Result = assessment;
            _responseDto.IsSuccess = true;
            return Ok(_responseDto);

        }



        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] CreateCodingAssessmentDto company)
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

            CodingAssessment companyToSave = _mapper.Map<CodingAssessment>(company);
            try
            {   
                Nexus? n = await _ApplicationDbContext.Nexus.FirstOrDefaultAsync(_ => _.NexusId ==  new Guid(company.AssociatedNexusId));
                companyToSave.Nexus = n;
                _ApplicationDbContext.CodingAssessments.Add(companyToSave);
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
