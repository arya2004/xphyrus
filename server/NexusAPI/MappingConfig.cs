using AutoMapper;
using NexusAPI.Dto;
using NexusAPI.Dto.StudentDto;
using NexusAPI.Dto.TeacherDto;
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


                config.CreateMap<StudentAnswerMetadata, ExamOverviewDto>()
            .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.StudentAnswerMetadataId))
            .ForMember(dest => dest.TestTitle, opt => opt.MapFrom(src => src.Test.Title));

                // Map StudentAnswerMetadata to ExamDetailsDto
                config.CreateMap<StudentAnswerMetadata, ExamDetailsDto>()
                    .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.StudentAnswerMetadataId));

                // Map StudentAnswer to StudentAnswerDto
                config.CreateMap<StudentAnswer, StudentAnswerDto>()
                    .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.CodingQuestion.Description));

                // Map Test to TestDto
                config.CreateMap<Test, DetailTestDto>();

                // Map CodingQuestion to CodingQuestionDto
                config.CreateMap<CodingQuestion, DetailCodingQuestionDto>();

                config.CreateMap<StudentAnswerMetadata, StudentAnswerMetadataDto>();
                
            });
            return mappingCOnfig;
        }
    }
}
