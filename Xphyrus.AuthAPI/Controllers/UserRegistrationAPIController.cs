using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xphyrus.AuthAPI.Data;
using Xphyrus.AuthAPI.Models;
using Xphyrus.AuthAPI.Models.Dto;
using Xphyrus.AuthAPI.Models.ResReq;
using Xphyrus.AuthAPI.Service.IService;
using Xphyrus.SubmissionAPI.Models.Dtos;

namespace Xphyrus.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected ResponseDto _responseDto;
        private readonly IAssesmentService _assesmentService;
        public UserRegistrationAPIController(ApplicationDbContext applicationDbContext, IAssesmentService assesmentService)
        {
            _applicationDbContext = applicationDbContext;
            _responseDto = new ResponseDto();
            
            _assesmentService = assesmentService;
        }
        //invoked by ass api
        [HttpPost("CreateAdminAssesment")]
        public async Task<ActionResult<ResponseDto>> CreateAdminAssesment(AssesmentAdminDto assesmentAdminDto)
        {
            AssesmentAdmins assesmentAdmins = new AssesmentAdmins()
            {
                ApplicationUser = assesmentAdminDto.ApplicationUserEmail,
                AssesmentId = assesmentAdminDto.AssesmentId,
                HasResultDeclared = false

            };
            await _applicationDbContext.AssesmentAdmins.AddAsync(assesmentAdmins);
            await _applicationDbContext.SaveChangesAsync();
            _responseDto.Result = true;
            return _responseDto;
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
        public async Task<ActionResult<ResponseDto>> RegisterForAssesment(string AssesmentCode)
        {
            _responseDto = await _assesmentService.RegisterForAssesment(AssesmentCode);
            if(_responseDto.IsSuccess && _responseDto.Result != "")
            {
                AssesmentParticipant assesmentParticipant = new AssesmentParticipant()
                {
                    ApplicationUser = "will_get_from_context_later",
                    AssesmentId = _responseDto.Result.ToString(),
                    HasCompleted = false,
                    HasStarted = false,
                };
                await _applicationDbContext.AssesmentParticipants.AddAsync(assesmentParticipant);
                await _applicationDbContext.SaveChangesAsync();
                _responseDto.Result = true;
            }

           
            return _responseDto;
        }
        [HttpPost("Start")]
        public async Task<ActionResult<ResponseDto>> StartAssesment([FromBody]StartAssesmentDto startAssesmentDto)
        {
            try
            {
                AssesmentParticipant? assesmentParticipant = await _applicationDbContext.AssesmentParticipants.SingleOrDefaultAsync(u => (u.AssesmentId == startAssesmentDto.AssesmentId && u.ApplicationUser == startAssesmentDto.UserEmail));
                if (assesmentParticipant == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "unable to find single";
                    return _responseDto;
                }
                if(assesmentParticipant.HasStarted == false && assesmentParticipant.HasCompleted == false)
                {
                    assesmentParticipant.HasStarted = true;
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
    }
}
