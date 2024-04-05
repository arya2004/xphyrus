using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service;
using NexusAPI.Service.IService;
using System.Security.Claims;



namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingAssessmentController : ControllerBase
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICodingAssessmentService _codingAssessmentService;
        public CodingAssessmentController(ICodingAssessmentService codingAssessment,  ApplicationDbContext ApplicationDbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _ApplicationDbContext = ApplicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _codingAssessmentService = codingAssessment;
        }
        [HttpGet("GetAll")]
        public ActionResult<ResponseDto> GetAll()
        {
           
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if(!_responseDto.IsSuccess) return _responseDto;

            List<CodingAssessment> companies = _ApplicationDbContext.CodingAssessments.ToList();
            _responseDto.Result = companies;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }

        [HttpGet("GetAllForNexus")]
        public async Task<ActionResult<ResponseDto>> GetAllForNexus(string NexusId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _codingAssessmentService.GetAllForNexus(HttpContext, new Guid(NexusId));
            return _responseDto;
        }







        [HttpGet("GetOne")]

        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            CodingAssessment? assessment = await _ApplicationDbContext.CodingAssessments.FirstOrDefaultAsync(_ => _.CodingAssessmentId == new Guid(id));
            _responseDto.Result = assessment;
            _responseDto.IsSuccess = true;
            return Ok(_responseDto);

        }



        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] CreateCodingAssessmentDto company)
        {

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;


            CodingAssessment companyToSave = _mapper.Map<CodingAssessment>(company);
            _responseDto = await _codingAssessmentService.Create(HttpContext, companyToSave, new Guid( company.AssociatedNexusId));
            return _responseDto;

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

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;


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
