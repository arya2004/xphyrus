namespace Xphyrus.CreationAPI.Models
{
    public class MSQ
    {
        public string Question { get; set; }
        public string[] Choices { get; set; }
        public int[] Corrects { get; set; }
    }
}