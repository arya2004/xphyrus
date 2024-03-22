
using Xphyrus.NexusAPI.Models.Dto;
using Xphyrus.NexusAPI.Models.ResReq;

namespace Xphyrus.NexusAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> ToCreateAssesmentAdmin(AssesmentAdminDto assesmentAdminDto);
        Task<ResponseDto> ToCreateAssesmentParticipant(AssesmentParticipantDto assesmentParticipant);
        Task<ResponseDto> ToStartAssesment(StartAssesmentDto startAssesmentDto);
    }
}
