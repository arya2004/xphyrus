using Xphyrus.CreationAPI.Models;

namespace Xphyrus.CreationAPI.Service.IService
{
    public interface ISpacesService
    {
        Task<Spaces> CreateSpace(Spaces spaces);
        Task<Spaces> GetSpaces(string spackId);
        Task<Spaces> UpdateSpace(Spaces spaces);
        Task<bool> DeleteSpace(string spackId);

    }
}
