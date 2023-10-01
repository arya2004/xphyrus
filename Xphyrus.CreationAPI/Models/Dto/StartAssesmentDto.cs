namespace Xphyrus.AssesmentAPI.Models.Dto
{
    public class StartAssesmentDto
    {   
        //should be assesment code
        public string? AssesmentCode { get; set; }
        //will be removed and token will be created
        public string UserEmail { get; set; }
    }
}
