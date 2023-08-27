using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.AssesmentAPI.Models.ResReq;
using Xphyrus.CreationAPI.Data;
using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Service.IService;

namespace Xphyrus.CreationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssesmentController : ControllerBase
    {
        private readonly ApplicatioDbContext _applicatioDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public AssesmentController(ApplicatioDbContext applicatioDbContext, IMapper mapper)
        {
            _applicatioDbContext = applicatioDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ResponseDto> GetAssesments(int assesmentId)
        {
            try
            {
                Assesment? assesment = _applicatioDbContext.Assesments.FirstOrDefault(u => u.AssesmentId == assesmentId);
                _responseDto.Result = assesment;
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
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.ToString();
            }
            return _responseDto; 
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> DeleteAssesment(int assesmentId )
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
    }
}
