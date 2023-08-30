
using Xphyrus.AssesmentAPI.Models.Dto;
using Xphyrus.AssesmentAPI.Models.ResReq;

namespace Xphyrus.AssesmentAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> ToCreateAssesmentAdmin(AssesmentAdminDto assesmentAdminDto);
        Task<ResponseDto> ToCreateAssesmentParticipant(AssesmentParticipantDto assesmentParticipant);
        Task<ResponseDto> ToStartAssesment(StartAssesmentDto startAssesmentDto);
    }
}
