using AutoMapper;
using Xphyrus.AuthAPI.Models;
using Xphyrus.AuthAPI.Models.Dto;

namespace Xphyrus.AuthAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AssesmentAdminDto, AssesmentAdmins>().ReverseMap();
            });
                return mappingCOnfig;
        }
    }
}
