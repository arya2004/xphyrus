using SubmissionAPI.Models.Dtos;
using SubmissionAPI.Models.ResReq;

namespace SubmissionAPI.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto> MarkSubmission(SubmissionDto regStartNotSubDto);


    }
}
