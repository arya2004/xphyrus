using NexusAPI.Dto;

namespace NexusAPI.Service.IService
{
    public interface IAuthorizationService
    {

        public ResponseDto VerifyToken(HttpContext httpContext);
        public ResponseDto VerifyRole(HttpContext httpContext, string role);
    }
}
