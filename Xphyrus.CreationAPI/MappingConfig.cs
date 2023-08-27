using AutoMapper;
using Xphyrus.CreationAPI.Models;
using Xphyrus.CreationAPI.Models.Dto;

namespace Xphyrus.AssesmentAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AdminAssesmentDto, Assesment>().ReverseMap();
            });
                return mappingCOnfig;
        }
    }
}
