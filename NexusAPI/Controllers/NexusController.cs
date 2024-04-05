using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service;
using NexusAPI.Service.IService;
using NuGet.Common;
using System.Security.Claims;


namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NexusController : ControllerBase
    {

        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly INexusService _nexusService;

        public NexusController(IMapper mapper, IAuthorizationService authorizationService, INexusService nexusService )
        {

            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _nexusService = nexusService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseDto>> GetAll()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _nexusService.GetAll(this.HttpContext);
            return _responseDto;
        }

        [HttpGet("GetMy")]
        public async Task<ActionResult<ResponseDto>> GetMy()
        {
            
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _nexusService.GetMy(this.HttpContext);
            return _responseDto;
        }

        [HttpPost("Join")]
        public async Task<ActionResult<ResponseDto>> Join(string nexusCode)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _nexusService.Join(this.HttpContext, nexusCode);
            return _responseDto;
        }





        [HttpGet("GetOne")]

        public async Task<ActionResult<ResponseDto>> Get(Guid id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;


            _responseDto = await _nexusService.Get(this.HttpContext, id);
            return _responseDto;

        }



        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] NexusDto company)
        {

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            Nexus nexusToSave = _mapper.Map<Nexus>(company);

            _responseDto = await _nexusService.Create(this.HttpContext, nexusToSave);
            return _responseDto;

        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> Edit(Nexus company)
        {

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

           _responseDto = await _nexusService.Edit(this.HttpContext, company);
            return _responseDto;

        }


        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {

            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;


            _responseDto = await _nexusService.Delete(this.HttpContext, id);
            return _responseDto;

        }
    }
}