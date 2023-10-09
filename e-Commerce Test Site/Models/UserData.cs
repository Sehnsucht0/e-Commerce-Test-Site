using System.ComponentModel.DataAnnotations.Schema;

namespace e_Commerce_Test_Site.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
        public string? Currency { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<CartItems>? CartItems { get; set; }
    }
}
