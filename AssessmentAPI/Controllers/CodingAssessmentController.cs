using AssessmentAPI.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using AssessmentAPI.Models;
using AssessmentAPI.Dto;
using AssessmentAPI.Service.IService;

namespace AssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingAssessmentController : ControllerBase
    {

        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly ICodingAssessmentService _codingAssessmentService;
        public CodingAssessmentController(ICodingAssessmentService codingAssessmentService, IMapper mapper)
        {
           _codingAssessmentService = codingAssessmentService;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }


        [HttpGet("GetOne")]

        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            _responseDto = await _codingAssessmentService.Get(HttpContext, new Guid(id));
            return _responseDto;

        }




    }
}
