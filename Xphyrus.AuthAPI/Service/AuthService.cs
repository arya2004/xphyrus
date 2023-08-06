using Microsoft.AspNetCore.Identity;
using Xphyrus.AuthAPI.Data;
using Xphyrus.AuthAPI.Models;
using Xphyrus.AuthAPI.Models.Dto;
using Xphyrus.AuthAPI.Service.IService;

namespace Xphyrus.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService, SignInManager<ApplicationUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
           _roleManager = roleManager;
            _jwtService = jwtService;
            this.signInManager = signInManager;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<UserDto> Login(LoginRequestDto requestDto)
        {
           var user = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user == null)
            {
                return new UserDto()
                {
                    Email = null,
                    Displlayname = null,
                    Token = ""

                };
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);
            if (! result.Succeeded)
            {
                return new UserDto()
                {
                    Email = null,
                    Displlayname = null,
                    Token = ""

                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            return new UserDto
            {
                Email = requestDto.Email,
                Token = _jwtService.GenerateToken(user, roles),
                Displlayname = requestDto.Email
            };
        }

        public async Task<string> Register(RegisterRequestDto requestDto)
        {
            ApplicationUser user = new()
            {
                Email = requestDto.Email,
                UserName = requestDto.Name,
                DisplayName = requestDto.Email,
                NormalizedEmail = requestDto.Email.Normalize()

            };
            try
            {
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return "Error";
        }
    }
}
