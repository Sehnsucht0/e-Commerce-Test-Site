using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Components
{
    public class ChangeQuantityFromCart: ViewComponent
    {
        public IViewComponentResult Invoke(AddToCartViewModel model) => View(model);
    }
}
