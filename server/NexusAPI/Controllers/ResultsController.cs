using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {

        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IResultsService _resultsService;
        public ResultsController(IAuthorizationService authorizationService, IMapper mapper, IResultsService resultsService)
        {

            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _resultsService = resultsService;
        }




        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseDto>> GetAll()
        {

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _resultsService.GetAll(this.HttpContext);
            return _responseDto;
        }

        [HttpGet("GetAllForAssessment")]
        public async Task<ActionResult<ResponseDto>> GetAllForAssessment(string AssessmentId)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _resultsService.GetAllForAssessment(HttpContext, new Guid(AssessmentId));
            return _responseDto;
        }


        [HttpGet("GetOne")]

        public async Task<ActionResult<ResponseDto>> Get(Guid id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _resultsService.Get(this.HttpContext, id);
            return _responseDto;

        }




        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;


            _responseDto = await _resultsService.Delete(this.HttpContext, id);
            return _responseDto;

        }

    }
}
