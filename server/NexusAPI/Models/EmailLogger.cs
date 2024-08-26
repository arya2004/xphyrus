namespace NexusAPI.Models
{


    public class EmailDetails
    {
        public string[] To { get; set; }
        public string[] CC { get; set; }
        public string[] Bcc { get; set; }
        public string? Subject { get; set; }
        public string? Intent { get; set; } // Example values: "confirm-email", "reset-password", "result-declaration"
        public Dictionary<string, string> Info { get; set; } // Key-value pairs to replace in the template

    }
}
