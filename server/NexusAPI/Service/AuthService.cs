using Microsoft.AspNetCore.Identity;
using NexusAPI.Data;
using NexusAPI.Dto;
using NexusAPI.Models;
using NexusAPI.Service.IService;


namespace NexusAPI.Service
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
                    DisplayName = null,
                    Token = "",
                    Name = null,
                    PRN = null,
                    Division = null,
                    Batch = null,
                    Bio = null,
                    Role = null
                };
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);
            if (!result.Succeeded)
            {
                return new UserDto()
                {
                    Email = null,
                    DisplayName = null,
                    Token = "",
                    Name = null,
                    PRN = null,
                    Division = null,
                    Batch = null,
                    Bio = null,
                    Role = null
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            string roleName = roles.FirstOrDefault(); // Assuming the user has only one role

            return new UserDto
            {
                Email = user.Email,
                Token = _jwtService.GenerateToken(user, roles),
                DisplayName = user.DisplayName,
                Name = user.UserName,
                PRN = user.PRN,
                Division = user.Division,
                Batch = user.Batch,
                Bio = user.Bio,
                Role = roleName
            };
        }

        public async Task<string> Register(RegisterRequestDto requestDto)
        {
            ApplicationUser user = new()
            {
                Email = requestDto.Email,
                UserName = requestDto.DisplayName,
                DisplayName = requestDto.Name,
                NormalizedEmail = requestDto.Email.Normalize(),
                PRN = requestDto.PRN,
                Division = requestDto.Division,
                Batch = requestDto.Batch,
                Bio = requestDto.Bio,
                Type = (UserRole)requestDto.UserRole // Casting the int Role to UserRole enum
            };

            try
            {
                // Create the user
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    // Determine the role name based on the numeric role value
                    string roleName = requestDto.UserRole switch
                    {
                        1 => "Student",
                        2 => "Teacher",
                        3 => "Admin",
                        _ => throw new ArgumentException("Invalid role value")
                    };

                    // Check if the role exists, if not, create it
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }

                    // Assign the role to the user
                    await _userManager.AddToRoleAsync(user, roleName);

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault()?.Description ?? "Unknown error";
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                throw;
            }
        }


       


    }
}
