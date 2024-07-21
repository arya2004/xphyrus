using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace NexusAPI.Models
{
    public class CodingAssessment
    {
        public Guid CodingAssessmentId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public int TotalPoints { get; set; }
        public bool IsPublished { get; set; }
        public ICollection<CodingQuestion> CodingQuestions { get; set; } = new List<CodingQuestion>();
        public ICollection<CodingAssessmentResult> CodingAssessmentResults { get; set; } = new List<CodingAssessmentResult>();
    }




}







