namespace Xphyrus.AssesmentAPI.Models.Dto
{
    public class AssesmentAdminDto
    {
        public string? ApplicationUserEmail { get; set; }
        public string? AssesmentId { get; set; }
        public bool HasResultDeclared { get; set; }
    }
}
