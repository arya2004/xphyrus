using AutoMapper;
using NexusAPI.Dto;
using NexusAPI.Models;

namespace NexusAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {

                config.CreateMap<NexusDto, Nexus>().ReverseMap();
                config.CreateMap<CreateCodingAssessmentDto, CodingAssessment>().ReverseMap();
            });
            return mappingCOnfig;
        }
    }
}
