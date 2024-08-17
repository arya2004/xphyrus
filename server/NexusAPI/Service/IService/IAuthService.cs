using NexusAPI.Dto;

namespace NexusAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto requestDto);
        Task<UserDto> Login(LoginRequestDto requestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
