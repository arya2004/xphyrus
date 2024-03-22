

using Xphyrus.ResultAPI.Models.ResReq;

namespace Xphyrus.ResultAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> ToCreateAssesmentAdmin();
        Task<ResponseDto> ToCreateAssesmentParticipant();
        Task<ResponseDto> ToStartAssesment();
    }
}
