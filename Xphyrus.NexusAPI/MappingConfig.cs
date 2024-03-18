using AutoMapper;
using Xphyrus.NexusAPI.Models;
using Xphyrus.NexusAPI.Models.Dto;

namespace Xphyrus.NexusAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
               //config.CreateMap<AssesmentDto, Assesment>().ReverseMap();
               // config.CreateMap<CodingDto, Coding>().ReverseMap();
                //config.CreateMap<EvaluationCaseDto, TestCase>().ReverseMap();
                //config.CreateMap<CodingAssesmentDto, CodingAssesment>().ReverseMap();

            });
                return mappingCOnfig;
        }
    }
}
