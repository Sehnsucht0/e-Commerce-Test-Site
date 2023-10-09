using System.ComponentModel.DataAnnotations;

namespace e_Commerce_Test_Site.Models
{
    public class SigninViewModel
    {
        [Required]
        [StringLength(15)]
        public string? Username { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool PersistentCookie { get; set; }
    }
}
