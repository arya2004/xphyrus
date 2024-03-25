using Xphyrus.NexusService.Models.ResReq;

namespace Xphyrus.NexusService.Service.IService
{
    public interface IAssesmentService
    {
        public Task<ResponseDto> RegisterForAssesment(string code);

    }
}
