using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IClassroomService _classroomService;
        private readonly ICachingService _cachingService;

        public ClassroomController(ICachingService cachingService, IMapper mapper, IAuthorizationService authorizationService, IClassroomService classroomService)
        {
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _classroomService = classroomService;
            _cachingService = cachingService;
        }

        [HttpGet("GetCache")]
        public async Task<ActionResult<ResponseDto>> GetCache(string id)
        {
            var obj = await _cachingService.GetCached(id);
            _responseDto.Result = obj;
            return _responseDto;
        }

        [HttpGet("SetCache")]
        public async Task<ActionResult<ResponseDto>> SetCache(string thingToCache)
        {
            bool success = await _cachingService.Cache(Guid.NewGuid().ToString(), thingToCache, TimeSpan.FromHours(20));
            _responseDto.IsSuccess = success;
            return _responseDto;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseDto>> GetAll()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _classroomService.GetAll(this.HttpContext);
            return _responseDto;
        }

        [HttpGet("GetMy")]
        public async Task<ActionResult<ResponseDto>> GetMy()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _classroomService.GetMy(this.HttpContext);
            return _responseDto;
        }


        [HttpGet("GetOne")]
        public async Task<ActionResult<ResponseDto>> Get(Guid id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _classroomService.Get(this.HttpContext, id);
            return _responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] ClassroomDto classroomDto)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            Classroom classroomToSave = _mapper.Map<Classroom>(classroomDto);

            _responseDto = await _classroomService.Create(this.HttpContext, classroomToSave);
            return _responseDto;
        }



        [HttpDelete]
        [ActionName("Delete")]
        public async Task<ActionResult<ResponseDto>> Delete(Guid id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _classroomService.Delete(this.HttpContext, id);
            return _responseDto;
        }
    }
}
