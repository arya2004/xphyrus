using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Models.Dto;

namespace Xphyrus.CreationAPI.Service.IService
{
    public interface ISpacesService
    {
        Task<Assesment> CreateSpace(Assesment spaces);
        
        Task<AdminSpacesDto> GetAdminSpaces(string spackId);
        Task<Assesment> GetStudentSpaces(string spackId);
        Task<Assesment> UpdateSpace(Assesment spaces);
        Task<bool> DeleteSpace(string spackId);


    }
}
