using NexusService.Models.ResReq;

namespace NexusService.Service.IService
{
    public interface IAssesmentService
    {
        public Task<ResponseDto> RegisterForAssesment(string code);

    }
}
