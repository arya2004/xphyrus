using Xphyrus.AuthAPI.Models.ResReq;

namespace Xphyrus.AuthAPI.Service.IService
{
    public interface IAssesmentService
    {
        public Task<ResponseDto> RegisterForAssesment(string code);

    }
}
