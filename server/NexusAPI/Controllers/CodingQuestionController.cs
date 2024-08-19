using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;

namespace NexusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodingQuestionController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICodingQuestionService _codingQuestionService;

        public CodingQuestionController(ICodingQuestionService codingQuestionService, ApplicationDbContext applicationDbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authorizationService = authorizationService;
            _codingQuestionService = codingQuestionService;
        }

        [HttpGet("GetAll")]
        public ActionResult<ResponseDto> GetAll()
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            List<CodingQuestion> questions = _applicationDbContext.CodingQuestions.ToList();
            _responseDto.Result = questions;
            _responseDto.IsSuccess = true;
            return _responseDto;
        }

        [HttpGet("GetAllByDifficulty")]
        public async Task<ActionResult<ResponseDto>> GetAllByDifficulty(int difficulty)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            _responseDto = await _codingQuestionService.GetAllByDifficulty(HttpContext, (Difficulty)difficulty);
            return _responseDto;
        }

        [HttpGet("GetOne")]
        public async Task<ActionResult<ResponseDto>> Get(string id)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (_responseDto.IsSuccess)
            {
                CodingQuestion? question = await _applicationDbContext.CodingQuestions.FirstOrDefaultAsync(_ => _.CodingQuestionId == new Guid(id));
                _responseDto.Result = question;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }

            _responseDto = await _codingQuestionService.GetAsync(HttpContext, new Guid(id));
            return _responseDto;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] CodingQuestionDto questionDto)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return _responseDto;

            CodingQuestion questionToSave = _mapper.Map<CodingQuestion>(questionDto);
            _responseDto = await _codingQuestionService.Create(HttpContext, questionToSave);
            return _responseDto;
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CodingQuestion question)
        {
            _responseDto = _authorizationService.VerifyToken(this.HttpContext);
            if (!_responseDto.IsSuccess) return BadRequest(_responseDto);

            try
            {
                _applicationDbContext.CodingQuestions.Update(question);
                await _applicationDbContext.SaveChangesAsync();
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
                CodingQuestion? question = _applicationDbContext.CodingQuestions.FirstOrDefault(_ => _.CodingQuestionId == id);
                if (question == null)
                {
                    _responseDto.Message = "Not Found";
                    _responseDto.IsSuccess = false;
                    return NotFound(_responseDto);
                }
                _applicationDbContext.CodingQuestions.Remove(question);
                await _applicationDbContext.SaveChangesAsync();
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
