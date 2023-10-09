using Microsoft.AspNetCore.Mvc;
using e_Commerce_Test_Site.Models;

namespace e_Commerce_Test_Site.Controllers
{
    public class ProductController : Controller
    {
        [Route("[controller]/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            Product product = await APIGetter.GetProductById(id);
            return View(product);
        }
    }
}
