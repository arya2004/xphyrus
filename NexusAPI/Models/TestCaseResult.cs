namespace NexusAPI.Models
{
    public class TestCaseResult
    {
        public Guid TestCaseResultId { get; set; } = Guid.NewGuid();
        public Guid TestCaseId { get; set; }
        public TestCase? TestCase { get; set; }
        public bool Passed { get; set; }
    }

}
