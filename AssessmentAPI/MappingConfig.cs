using AutoMapper;


namespace AssessmentAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingCOnfig = new MapperConfiguration(config =>
            {
               
                //config.CreateMap<CodingAssesmentDto, CodingAssesment>().ReverseMap();

            });
            return mappingCOnfig;
        }
    }
}
