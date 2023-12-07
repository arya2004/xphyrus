using AutoMapper;
using Xphyrus.AssesmentAPI.Models;
using Xphyrus.AssesmentAPI.Models.Dto;


namespace Xphyrus.AssesmentAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
               //config.CreateMap<AssesmentDto, Assesment>().ReverseMap();
               // config.CreateMap<CodingDto, Coding>().ReverseMap();
                config.CreateMap<EvaluationCaseDto, EvaluationCase>().ReverseMap();
                config.CreateMap<CodingAssesmentDto, CodingAssesment>().ReverseMap();

            });
                return mappingCOnfig;
        }
    }
}
