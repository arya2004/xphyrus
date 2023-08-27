namespace Xphyrus.CreationAPI.Models.Dto
{
    public class AdminAssesmentDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool IsStrict { get; set; }
        public string[] Admins { get; set; }
       
        public string[] Joined { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
 
        public List<Coding>? Codings { get; set; }
    }
}
