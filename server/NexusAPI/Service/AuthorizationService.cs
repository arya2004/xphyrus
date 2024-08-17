
using NexusAPI.Dto;
using NexusAPI.Service.IService;
using System.Security.Claims;

namespace NexusAPI.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        public ResponseDto VerifyRole(HttpContext httpContext, string role)
        {
            var roles = httpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            if (roles == null)
            {
                return new ResponseDto(false, "Invalid Token Content");
            }
            if (!roles.Contains(role))
            {
                return new ResponseDto(false, "Unauthorized");
            }
            return new ResponseDto(true, "Success");
        }

        public ResponseDto VerifyToken(HttpContext httpContext)
        {
            var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var roles = httpContext.User.FindAll(ClaimTypes.Role)?.Select(c => c.Value).ToList();
            ResponseDto _responseDto = new ResponseDto(true, "");

            if (roles == null || roles.Count == 0 || email == null)
            {
                return new ResponseDto(false, "Invalid Token");
            }
            _responseDto =  VerifyRole(httpContext, "ADMIN");
            return _responseDto;
            
        }
    }
}
