using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace e_Commerce_Test_Site.Models
{
    public class Product
    {
        [JsonIgnore]
        public int IdAPI { get; set; }

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal Rating { get; set; }

        public int Stock { get; set; }

        public string? Brand { get; set; }

        public string? Category { get; set; }

        public string? Thumbnail { get; set; }

        [NotMapped]
        public string[]? Images { get; set; }

        [JsonIgnore]
        public ICollection<UserOrder>? UserOrders { get; set; }
    }
}
