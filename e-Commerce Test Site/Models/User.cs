using Microsoft.AspNetCore.Identity;

namespace e_Commerce_Test_Site.Models
{
    public class User: IdentityUser
    {
        public UserData? UserData { get; set; }
    }
}
