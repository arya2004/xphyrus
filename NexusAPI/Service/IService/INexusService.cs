using NexusAPI.Dto;
using NexusAPI.Models;


namespace NexusAPI.Service.IService
{
    public interface INexusService
    {
        public Task<ResponseDto> Join(HttpContext httpContext, string nexusCode);

        public Task<ResponseDto> Create(HttpContext httpContext, Nexus nexus);

        public Task<ResponseDto> Edit(HttpContext httpContext, Nexus nexus);

        public Task<ResponseDto> Delete(HttpContext httpContext, Guid nexusId);
        public Task<ResponseDto> Get(HttpContext httpContext, Guid nexusId);
        public Task<ResponseDto> GetMy(HttpContext httpContext);
        public Task<ResponseDto> GetAll(HttpContext httpContext);


    }
}
