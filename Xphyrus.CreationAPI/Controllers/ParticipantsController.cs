using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xphyrus.AssesmentAPI.Data;
using Xphyrus.AssesmentAPI.Models;
using Xphyrus.AssesmentAPI.Models.Dto;
using Xphyrus.AssesmentAPI.Models.ResReq;
using Xphyrus.AssesmentAPI.Service.IService;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Xphyrus.AssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ApplicatioDbContext _applicationDbContext;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        private readonly IAuthService _authService;
        public ParticipantsController(ApplicatioDbContext applicatioDbContext, IMapper mapper, IAuthService authService)
        {
            _applicationDbContext = applicatioDbContext;
            _responseDto = new ResponseDto();
            _mapper = mapper;
            _authService = authService;
        }

        //feels wrong
        //[HttpPost("CreateParticipantAssesment")]
        //private async Task<ActionResult<ResponseDto>> CreateParticipantAssesment(AssesmentParticipantDto assesmentParticipantDto)
        //{
        //    AssesmentParticipant assesmentParticipant = new AssesmentParticipant()
        //    {
        //        ApplicationUser = assesmentParticipantDto.ApplicationUserEmail,
        //        AssesmentId = assesmentParticipantDto.AssesmentId,
        //        HasCompleted = false,
        //        HasStarted = false,
        //    };
        //    await _applicationDbContext.AssesmentParticipants.AddAsync(assesmentParticipant);
        //    await _applicationDbContext.SaveChangesAsync();
        //    _responseDto.Result = true;
        //    return _responseDto;
        //}
        //invoked by auth
        [HttpPost("Register")]
        //check code
        //check if registers already
        //regisre
      //  [Authorize]
        
        public async Task<ActionResult<ResponseDto>> RegisterForAssesment([FromBody]string AssesmentCode)
        {

            var email = "a@s.com";// HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var participantId = "93yrwuiuwuehr3w";// HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (email == null || participantId == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "invalid token";
                return _responseDto;
            }


            CodingAssesment? assesment = _applicationDbContext.Assesments.FirstOrDefault(u => u.JoinCode == AssesmentCode);
            if (assesment == null)
            {   
                _responseDto.IsSuccess = false;
                _responseDto.Message = "cant find that assemsnt";
                return _responseDto; 
            }
            AssesmentParticipant assesmentParticipant = new AssesmentParticipant()
            {
                ApplicationUser = participantId,
                AssesmentId = assesment.CodingAssesmentId,
                HasCompleted = false,
                HasStarted = false,
            };
            await _applicationDbContext.AssesmentParticipants.AddAsync(assesmentParticipant);
            await _applicationDbContext.SaveChangesAsync();
            _responseDto.Result = true;
            


            return _responseDto;
        }
        [HttpPost("Start")]
        public async Task<ActionResult<ResponseDto>> StartAssesment([FromBody] string AssesmentCode)
        {
            var email = "a@s.com";// HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var participantId = "93yrwuiuwuehr3w";// HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (email == null || participantId == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "invalid token";
                return _responseDto;
            }


            try
            {
                AssesmentParticipant? assesmentParticipant = await _applicationDbContext.AssesmentParticipants.FirstOrDefaultAsync(u => (u.AssesmentId == AssesmentCode && u.ApplicationUser == participantId));
                if (assesmentParticipant == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "unable to find single";
                    return _responseDto;
                }
                if (assesmentParticipant.HasStarted == false && assesmentParticipant.HasCompleted == false)
                {
                    assesmentParticipant.HasStarted = true;
                    _applicationDbContext.AssesmentParticipants.Update(assesmentParticipant);
                    await _applicationDbContext.SaveChangesAsync();
                    CodingAssesment? assesmnet = await _applicationDbContext.Assesments.Where(u => u.CodingAssesmentId == assesmentParticipant.AssesmentId).FirstOrDefaultAsync();
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = assesmnet;
                    return _responseDto;
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = "already started or submitted";
                return _responseDto;


            }
            catch (Exception ex)
            {

                _responseDto.Message = ex.Message;
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }
        [HttpPost("Submit")]
        public async Task<ActionResult<ResponseDto>> Submit([FromBody] SubmissionDto submissionDto)
        {
            try
            {
                AssesmentParticipant? assesmentParticipant = await _applicationDbContext.AssesmentParticipants.SingleOrDefaultAsync(u => (u.AssesmentId == submissionDto.AssesmentId && u.ApplicationUser == submissionDto.UserEmail));
                if (assesmentParticipant == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "didnt registered or unable to find single";
                    return _responseDto;
                }
                if (assesmentParticipant.HasStarted == true && assesmentParticipant.HasCompleted == false)
                {
                    assesmentParticipant.HasCompleted = true;
                    _applicationDbContext.AssesmentParticipants.Update(assesmentParticipant);
                    await _applicationDbContext.SaveChangesAsync();
                    _responseDto.IsSuccess = true;
                    return _responseDto;
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = "already started or submitted";
                return _responseDto;
            }
            catch (Exception ex)
            {

                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpGet("Joined")]
        //[Authorize(Roles ="ADMIN")]
        public async Task<ActionResult<ResponseDto>> GetJoinedAssesment()
        {
            var email = "a@s.com"; ; //HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var adminId = "93yrwuiuwuehr3w";// HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (email == null || adminId == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "invalid token";
            }
            try
            {
                IQueryable<AssesmentParticipant> adm = _applicationDbContext.AssesmentParticipants.Where(a => a.ApplicationUser == adminId);
                var primaryKeyValues = adm.Select(entity => entity.AssesmentId).ToArray();
                IQueryable<CodingAssesment> ass = _applicationDbContext.Assesments.Where(entity => primaryKeyValues.Contains(entity.CodingAssesmentId));

                _responseDto.IsSuccess = true;
                _responseDto.Result = ass;
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
