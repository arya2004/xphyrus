using Xphyrus.AuthAPI.Models;

namespace Xphyrus.AuthAPI.Service.IService
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);   
    }
}
