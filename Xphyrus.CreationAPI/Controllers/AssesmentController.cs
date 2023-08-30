using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xphyrus.AssesmentAPI.Models.Dto;
using Xphyrus.AssesmentAPI.Models.ResReq;
using Xphyrus.AssesmentAPI.Service.IService;
using Xphyrus.CreationAPI.Data;
using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Models.Dto;

namespace Xphyrus.CreationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssesmentController : ControllerBase
    {
        private readonly ApplicatioDbContext _applicatioDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly IAuthService _authService;
        public AssesmentController(ApplicatioDbContext applicatioDbContext, IMapper mapper, IAuthService authService)
        {
            _applicatioDbContext = applicatioDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authService = authService;
        }
        
        [HttpGet]
        public async Task<ResponseDto> GetAssesments(string assesmentCode)
        {
            try
            {
                Assesment? assesment = _applicatioDbContext.Assesments.FirstOrDefault(u => u.Code == assesmentCode);
                if (assesment == null)
                {
                    _responseDto.IsSuccess = false;
                    return _responseDto;
                }
                _responseDto.Result = assesment;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        //invoked by auth service
        [HttpGet("CheckIfAssesmentExist/{assesmentCode}")]
        public async Task<ResponseDto> CheckIfAssesmentExist(string assesmentCode)
        {
            try
            {
                Assesment? assesment = await _applicatioDbContext.Assesments.FirstOrDefaultAsync(u => u.Code == assesmentCode);
                if (assesment == null)
                {
                    _responseDto.IsSuccess = false;
                    return _responseDto;
                }
                _responseDto.Result = assesmentCode;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
        [HttpPost]
        public async Task<ResponseDto> CreateAssesments([FromBody]Assesment assesment)
        {
            
            try
            {
                _applicatioDbContext.Assesments.Add(assesment);
                _applicatioDbContext.SaveChanges();
                Assesment assesFromDb = await _applicatioDbContext.Assesments.FirstOrDefaultAsync(u => u.Code == assesment.Code);
                AssesmentAdminDto assesmentAdminDto = new AssesmentAdminDto()
                {
                    AssesmentId = assesFromDb.AssesmentId,
                    ApplicationUserEmail = "temp@t.com",
                    HasResultDeclared = false

                };
                _responseDto = await _authService.ToCreateAssesmentAdmin(assesmentAdminDto);
                

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.ToString();
            }
            return _responseDto; 
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> DeleteAssesment(string assesmentId )
        {
            

            try
            {
                Assesment? assesment = _applicatioDbContext.Assesments.First(u => u.AssesmentId == assesmentId);
                _applicatioDbContext.Remove(assesment);
                _applicatioDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateAssesment([FromBody]Assesment assesment)
        {
            
            try
            {
                _applicatioDbContext.Update(assesment);
                _applicatioDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }

        [HttpPost("StartAssesment")]
        public async Task<ActionResult<ResponseDto>> StartAssesment(StartAssesmentDto startAssesmentDto)
        {
            try
            {
                _responseDto = await _authService.ToStartAssesment(startAssesmentDto);
                if (!_responseDto.IsSuccess)
                {
                    return _responseDto;
                }
                //start now
                Assesment? assesment = await _applicatioDbContext.Assesments.Include(i => i.Codings).ThenInclude(c => c.Examples).SingleOrDefaultAsync(u => u.Code == startAssesmentDto.AssesmentId);
                _responseDto.Result = assesment;
                _responseDto.IsSuccess = true;
                return _responseDto;
            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess=false;
            }
            return _responseDto;
        }
    }
}
