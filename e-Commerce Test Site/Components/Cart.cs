using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Components
{
    public class Cart : ViewComponent
    {
        public IViewComponentResult Invoke(int count) => View(count);
    }
}
