namespace EvaluationService.Models
{
    public class EmailLogger
    {

        public EmailLogger()
        {
            To = new List<string>();
        }
        public List<string>? To { get; set; } // List of email addresses of recipients
        public List<string>? Cc { get; set; } // List of email addresses to CC
        public List<string>? Bcc { get; set; } // List of email addresses to BCC
        public string? Subject { get; set; } // Email subject
        public string? Body { get; set; }
    }
}
