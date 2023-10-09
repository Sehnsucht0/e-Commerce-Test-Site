namespace e_Commerce_Test_Site.Models
{
    public class AdminOrder
    {
        public string Command { get; set; } = string.Empty;
        public List<int> OrderIds { get; set; } = new List<int>();
    }
}
