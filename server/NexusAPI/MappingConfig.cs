using AutoMapper;
using NexusAPI.Dto;
using NexusAPI.Dto.StudentDto;
using NexusAPI.Models;

namespace NexusAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {


               
                config.CreateMap<Classroom, ClassroomDto>().ReverseMap();
                config.CreateMap<TestDto, Test>().ReverseMap();
                config.CreateMap<CodingQuestionDto, CodingQuestion>().ReverseMap();
                config.CreateMap<TestCaseDto, TestCase>().ReverseMap();

                config.CreateMap<StudentTestCaseDto, TestCase>().ReverseMap();
                config.CreateMap<StudentQuestionDto, CodingQuestion>().ReverseMap();
                config.CreateMap<StudentTestDto, Test>().ReverseMap();
            });
            return mappingCOnfig;
        }
    }
}
