using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Xphyrus.AuthAPI.Models.Dto;
using Xphyrus.AuthAPI.Models.ResReq;
using Xphyrus.AuthAPI.Service.IService;

namespace Xphyrus.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        protected ResponseDto _responseDto;
        public AuthenticationAPIController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
            _responseDto = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var Message = await _authService.Register(registerRequestDto);
            if(!string.IsNullOrEmpty(Message))
            {
                _responseDto.Message = Message;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);

        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var Message = await _authService.Login(loginRequestDto);
            if (Message.Email == null)
            {
                _responseDto.IsSuccess=false;
                _responseDto.Message = "incorrect field";
                return BadRequest(_responseDto);    
            }
            _responseDto.Result = Message;
            return _responseDto;
        }

        [HttpPost("assign")]
        public async Task<ActionResult<ResponseDto>> Assign([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _authService.AssignRole(registerRequestDto.Email, registerRequestDto.Role.ToUpper());
            if(!result) _responseDto.IsSuccess = false;
            return Ok(_responseDto);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> LoadCurrentUser()
        {
            var text = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                Displlayname = user.DisplayName

            };
        }
    }
}
