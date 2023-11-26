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
                CodingAssesment? assesment = _applicatioDbContext.Assesments.FirstOrDefault(u => u.JoinCode == assesmentCode);
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
                CodingAssesment? assesment = await _applicatioDbContext.Assesments.FirstOrDefaultAsync(u => u.JoinCode == assesmentCode);
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
      // [Authorize]
        public async Task<ResponseDto> CreateAssesments([FromBody] CodingAssesmentDto assesment)
        {
            var email = "a@s.com";// HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            Console.WriteLine(email);
            var adminId = "93yrwuiuwuehr3w"; //HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(email);

            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //Console.WriteLine(email);
            try
            {
                CodingAssesment toSave = _mapper.Map<CodingAssesment>(assesment);
                toSave.JoinCode = Guid.NewGuid().ToString();
                await _applicatioDbContext.Assesments.AddAsync(toSave);
                await _applicatioDbContext.SaveChangesAsync();
                CodingAssesment? assesFromDb = await _applicatioDbContext.Assesments.FirstOrDefaultAsync(u => u.JoinCode == toSave.JoinCode);
                if (assesFromDb == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "unble to find";
                    return _responseDto;
                }

                AssesmentAdmins assesmentAdmin = new AssesmentAdmins()
                {
                    AssesmentId = assesFromDb.CodingAssesmentId,
                    ApplicationUser = adminId,
                    HasResultDeclared = false

                };
                await _applicatioDbContext.AssesmentAdmins.AddAsync(assesmentAdmin);
                await _applicatioDbContext.SaveChangesAsync();
                _responseDto.IsSuccess = true;
                _responseDto.Message = "creatd";



            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.ToString() ?? ex.Message.ToString();
            }
            return _responseDto;
        }

        //[HttpDelete]
        //public async Task<ActionResult<ResponseDto>> DeleteAssesment(string assesmentId)
        //{


        //    try
        //    {
        //        CodingAssesment? assesment =  await _applicatioDbContext.Assesments.Include(a => a.EvaluationCases).FirstOrDefaultAsync(u => u.CodingAssesmentId == assesmentId);
        //        if (assesment == null)
        //        {
        //            _responseDto.Message = "cant find lol";
        //            _responseDto.IsSuccess = false;
        //            return _responseDto;
                    
        //        }
                
                
        //        AssesmentAdmins? assesmentAdmins = await _applicatioDbContext.AssesmentAdmins.Where(u => u.AssesmentId == assesmentId).FirstOrDefaultAsync();

        //        IQueryable<AssesmentParticipant> lel = _applicatioDbContext.AssesmentParticipants;
        //        var assesmentParticipants =  lel.Where(a => a.AssesmentId == assesmentId).ToList();

        //        if(assesment != null)
        //        {
        //           // _applicatioDbContext.Coding.Remove(assesment);
        //        }
               

        //        _applicatioDbContext.AssesmentParticipants.RemoveRange(assesmentParticipants);
        //        if (assesmentAdmins != null)
        //        {
        //            _applicatioDbContext.AssesmentAdmins.Remove(assesmentAdmins);
        //        }
               
        //       // _applicatioDbContext.Assesments.Remove(assesment);
        //        await _applicatioDbContext.SaveChangesAsync();

        //    }
        //    catch (Exception ex)
        //    {
        //        _responseDto.IsSuccess = false;
        //        _responseDto.Message = ex.Message;
        //    }
        //    return _responseDto;
        //}

        //[HttpPut]
        //public async Task<ActionResult<ResponseDto>> UpdateAssesment([FromBody] CodingAssesment assesment)
        //{

        //    try
        //    {
        //        //_applicatioDbContext.Assesments.Update(assesment);
        //       // await _applicatioDbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _responseDto.IsSuccess = false;
        //        _responseDto.Message = ex.Message;
        //    }
        //    return _responseDto;

        //}
        [HttpGet("created")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> GetAssesment()
        {
            var email = "a@s.com"; ; //HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var adminId = "93yrwuiuwuehr3w";// HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(email == null || adminId == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "invalid token";
            }
            try
            {
                IQueryable<AssesmentAdmins> adm = _applicatioDbContext.AssesmentAdmins.Where(a => a.ApplicationUser == adminId);
                var primaryKeyValues = adm.Select(entity => entity.AssesmentId).ToArray();
                IQueryable<CodingAssesment> ass = _applicatioDbContext.Assesments.Where(entity => primaryKeyValues.Contains(entity.CodingAssesmentId))  ;

                _responseDto.IsSuccess = true;
                _responseDto.Result = ass;
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess=false;
                _responseDto.Message = ex.Message;
            }
            


            return _responseDto;
        }

     
                //start now
              //  Assesment? assesment = await _applicatioDbContext.Assesments.Include(i => i.Codings).ThenInclude(c => c.Examples).SingleOrDefaultAsync(u => u.Code == startAssesmentDto.AssesmentCode);
               
        
    }
}
