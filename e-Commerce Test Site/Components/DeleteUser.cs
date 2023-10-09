using Microsoft.AspNetCore.Mvc;
using e_Commerce_Test_Site.Models;

namespace e_Commerce_Test_Site.Components
{
    public class DeleteUser : ViewComponent
    {
        public IViewComponentResult Invoke(string command, string UserId)
        {
            return View(new AdminAreaCommand() { Command = command, Id = UserId});
        }
    }
}
