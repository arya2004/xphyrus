using NexusAPI.Models;


namespace NexusAPI.Service.IService
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
