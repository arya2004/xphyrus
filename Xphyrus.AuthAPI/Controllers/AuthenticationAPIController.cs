using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xphyrus.AuthAPI.Models.Dto;
using Xphyrus.AuthAPI.Service.IService;

namespace Xphyrus.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthenticationAPIController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var Message = await _authService.Register(registerRequestDto);
            if(!string.IsNullOrEmpty(Message))
            {
                return BadRequest(Message);
            }
            return "successss";

        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var Message = await _authService.Login(loginRequestDto);
            
            return Message;
        }

        [HttpPost("assign")]
        public async Task<ActionResult<bool>> Assign([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _authService.AssignRole(registerRequestDto.Email, registerRequestDto.Role.ToUpper());

            return Ok(result);
        }
    }
}
