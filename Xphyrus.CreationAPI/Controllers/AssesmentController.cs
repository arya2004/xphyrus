using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net.Mail;
using System.Net;
using Xphyrus.AssesmentAPI.Data;
using Xphyrus.AssesmentAPI.Models;
using Xphyrus.AssesmentAPI.Models.Dto;
using Xphyrus.AssesmentAPI.Models.ResReq;
using Xphyrus.AssesmentAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Xphyrus.AssesmentAPI.Controllers
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
       [Authorize(Roles ="ADMIN")]
        public async Task<ResponseDto> CreateAssesments([FromBody] AssesmentDto assesment)
        {
            var text = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine(text);
          
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            Console.WriteLine(email);
            try
            {
                Assesment toSave = _mapper.Map<Assesment>(assesment);
                await _applicatioDbContext.Assesments.AddAsync(toSave);
                await _applicatioDbContext.SaveChangesAsync();
                Assesment? assesFromDb = await _applicatioDbContext.Assesments.FirstOrDefaultAsync(u => u.Code == assesment.Code);
                if (assesFromDb == null)
                {   
                    _responseDto.IsSuccess=false;
                    _responseDto.Message = "unble to find";
                    return _responseDto;
                }
                
                AssesmentAdmins assesmentAdminDto = new AssesmentAdmins()
                {
                    AssesmentId = assesFromDb.AssesmentId,
                    ApplicationUser = "temp@t.com",
                    HasResultDeclared = false

                };
                await _applicatioDbContext.AssesmentAdmins.AddAsync(assesmentAdminDto);
                await _applicatioDbContext.SaveChangesAsync();
       


            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.ToString() ?? ex.Message.ToString();
            }
            return _responseDto;
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> DeleteAssesment(string assesmentId)
        {


            try
            {
                Assesment? assesment =  await _applicatioDbContext.Assesments.Include(a => a.Codings).ThenInclude(a => a.Examples).Include(a => a.Codings).ThenInclude(a => a.Constrains).Include(a => a.Codings).ThenInclude(a => a.Code).Include(a => a.Codings).ThenInclude(a => a.EvliationCases).FirstOrDefaultAsync(u => u.AssesmentId == assesmentId);
                if (assesment == null)
                {
                    _responseDto.Message = "cant find lol";
                    _responseDto.IsSuccess = false;
                    return _responseDto;
                    
                }
                
                
                AssesmentAdmins? assesmentAdmins = await _applicatioDbContext.AssesmentAdmins.Where(u => u.AssesmentId == assesmentId).FirstOrDefaultAsync();

                IQueryable<AssesmentParticipant> lel = _applicatioDbContext.AssesmentParticipants;
                var assesmentParticipants =  lel.Where(a => a.AssesmentId == assesmentId).ToList();

                if(assesment.Codings != null)
                {
                    _applicatioDbContext.Coding.RemoveRange(assesment.Codings);
                }
               

                _applicatioDbContext.AssesmentParticipants.RemoveRange(assesmentParticipants);
                if (assesmentAdmins != null)
                {
                    _applicatioDbContext.AssesmentAdmins.Remove(assesmentAdmins);
                }
               
                _applicatioDbContext.Assesments.Remove(assesment);
                await _applicatioDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateAssesment([FromBody] Assesment assesment)
        {

            try
            {
                _applicatioDbContext.Assesments.Update(assesment);
               // await _applicatioDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;

        }

     
                //start now
              //  Assesment? assesment = await _applicatioDbContext.Assesments.Include(i => i.Codings).ThenInclude(c => c.Examples).SingleOrDefaultAsync(u => u.Code == startAssesmentDto.AssesmentCode);
               
        
    }
}
