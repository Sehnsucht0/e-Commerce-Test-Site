using e_Commerce_Test_Site.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Components
{
    public class RemoveFromCart: ViewComponent
    {
        public IViewComponentResult Invoke(RemoveFromCartDTO model) => View(model);
    }
}
