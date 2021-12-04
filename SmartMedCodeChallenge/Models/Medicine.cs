namespace SmartMedCodeChallenge.Models
{
    public class Medicine
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public int Quantity { get; set; }
    }
}
