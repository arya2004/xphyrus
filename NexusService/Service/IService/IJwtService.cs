using Xphyrus.NexusService.Models;

namespace Xphyrus.NexusService.Service.IService
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);   
    }
}
