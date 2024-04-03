using NexusService.Models;


namespace NexusService.Service.IService
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);   
    }
}
