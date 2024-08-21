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

                config.CreateMap<NexusDto, Classroom>().ReverseMap();
                config.CreateMap<CreateCodingAssessmentDto, CodingQuestion>().ReverseMap();
                config.CreateMap<CreateTestCase, TestCase>().ReverseMap();
                config.CreateMap<Classroom, ClassroomDto>().ReverseMap();
                config.CreateMap<TestDto, Test>().ReverseMap();
                config.CreateMap<CodingQuestionDto, CodingQuestion>().ReverseMap();
                config.CreateMap<TestCaseDto, TestCase>().ReverseMap();
            });
            return mappingCOnfig;
        }
    }
}
