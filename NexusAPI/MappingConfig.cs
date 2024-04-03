using AutoMapper;
using NexusService.Models;
using NexusService.Models.Dto;

namespace Xphyrus.NexusService
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<AssesmentAdminDto, AssesmentAdmins>().ReverseMap();
                config.CreateMap<NexusDto, Nexus>().ReverseMap();
                config.CreateMap<CreateCodingAssessmentDto, CodingAssessment>().ReverseMap();
            });
                return mappingCOnfig;
        }
    }
}
