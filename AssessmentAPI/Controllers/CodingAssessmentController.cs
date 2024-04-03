using AssessmentAPI.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using AssessmentAPI.Models;
using AssessmentAPI.Models.ResReq;

namespace AssessmentAPI.Controllers
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









        [HttpGet("GetOne")]

        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            try
            {
                CodingAssessment? assessment = await _ApplicationDbContext.CodingAssessments.FirstOrDefaultAsync(_ => _.CodingAssessmentId == new Guid(id));
                _responseDto.Result = assessment;
                _responseDto.IsSuccess = true;
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }


            return Ok(_responseDto);

        }




    }
}
