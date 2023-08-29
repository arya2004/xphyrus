using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.AuthAPI.Data;
using Xphyrus.AuthAPI.Models;
using Xphyrus.AuthAPI.Models.Dto;
using Xphyrus.AuthAPI.Models.ResReq;
using Xphyrus.AuthAPI.Service.IService;

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
    }
}
