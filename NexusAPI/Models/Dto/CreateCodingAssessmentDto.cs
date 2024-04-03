namespace NexusService.Models.Dto
{
    public class CreateCodingAssessmentDto
    {

        public string? Title { get; set; }

        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssociatedNexusId { get; set; }


    }
}
