using Microsoft.AspNetCore.Mvc;
using e_Commerce_Test_Site.Models;

namespace e_Commerce_Test_Site.Components
{
    public class AddToCart: ViewComponent
    {
        public IViewComponentResult Invoke(int ProductId, int stock)
        {
            return View(new AddToCartViewModel() { ProductId = ProductId, Stock = stock});
        }
    }
}
