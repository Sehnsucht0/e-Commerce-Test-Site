using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Controllers
{
    [Route("[controller]")]
    public class CategoryController: Controller
    {
        [HttpGet]
        [Route("{category}")]
        public async Task<IActionResult> Index (string category)
        {
            var products = await APIGetter.GetCategoryProductsAsync(category);
            return View("SearchResults", products);
        }
    }
}
