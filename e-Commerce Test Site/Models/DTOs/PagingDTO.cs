namespace e_Commerce_Test_Site.Models.DTOs
{
    public class PagingDTO
    {
        public int Page { get; set; }
        public int PageTotal { get; set; }
        public string PagingQuery { get; set; } = string.Empty;
    }
}
