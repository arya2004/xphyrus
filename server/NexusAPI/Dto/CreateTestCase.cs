namespace NexusAPI.Dto
{
    public class CreateTestCase
    {
 
        public string? InputCase { get; set; }
        public string? OutputCase { get; set; }
        public Guid AssociatedCodingAssessment { get; set; }
      
    }
}
