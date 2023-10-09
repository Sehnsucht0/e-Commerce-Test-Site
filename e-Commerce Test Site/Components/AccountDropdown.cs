using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Components
{
    public class AccountDropdown: ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
