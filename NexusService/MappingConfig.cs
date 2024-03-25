using AutoMapper;
using Xphyrus.NexusService.Models;
using Xphyrus.NexusService.Models.Dto;

namespace Xphyrus.NexusService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<AssesmentAdminDto, AssesmentAdmins>().ReverseMap();
            });
                return mappingCOnfig;
        }
    }
}
