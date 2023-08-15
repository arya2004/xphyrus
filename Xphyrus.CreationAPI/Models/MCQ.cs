using System.Reflection.Metadata.Ecma335;

namespace Xphyrus.CreationAPI.Models
{
    public class MCQ
    {
        public string Question { get; set; }
        public string[] Choices { get; set; }
        public int Correct { get; set; }
    }
}