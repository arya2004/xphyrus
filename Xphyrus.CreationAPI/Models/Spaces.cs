namespace Xphyrus.CreationAPI.Models
{
    public class Spaces
    {
        public Spaces()
        {
            
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string  Code { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<MCQ>? MCQs { get; set; }
        public List<MSQ>? MSQs { get; set; }
        public List<FITB>? FITBs { get; set; }
        public List<Essay>? Essays { get; set; }
        public List<Coding>? Codings { get; set; }
    }
}
