namespace Xphyrus.AssesmentAPI.Models.Dto
{
    public class IndexSpacesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsStrict { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
