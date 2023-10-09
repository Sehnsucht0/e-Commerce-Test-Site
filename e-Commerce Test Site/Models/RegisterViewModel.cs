using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace e_Commerce_Test_Site.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(15)]
        [Remote("CheckUsername", "Auth")]
        public string? Username { get; set; }
        [Required]
        [StringLength(40)]
        [EmailAddress]
        [Remote("CheckEmail", "Auth")]
        public string? Email { get; set; }
        [Required]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
