namespace NexusAPI.Dto
{
    public class RegisterRequestDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int UserRole { get; set; } // Now Role is an int to represent the numeric value
        public string? DisplayName { get; set; }
        public string? PRN { get; set; }
        public string? Division { get; set; }
        public string? Batch { get; set; }
        public string? Bio { get; set; }
    }
}
