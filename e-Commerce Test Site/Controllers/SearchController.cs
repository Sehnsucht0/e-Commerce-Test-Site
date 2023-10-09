using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Controllers
{
    public class SearchController: Controller
    {
        [Route("search")]
        [HttpPost]
        public IActionResult Index(SearchQuery model)
        {
            return RedirectToAction("Index", model);
        }

        [Route("search")]
        [HttpGet]
        public async Task<IActionResult> Index(string name, string category, int page)
        {
            const int pageMax = 7;
            var products = await APIGetter.GetSearchResultsAsync(name);
            if(category != "all") products = APIGetter.FilterCategory(products, category);

            TempData["PageTotal"] = Convert.ToInt32(Math.Ceiling(products.Length / Convert.ToDecimal(pageMax)));
            TempData["Page"] = page;
            TempData["PagingQuery"] = "?name=" + name + "&category=" + category + "&page=";

            products = products.Skip((page - 1) * pageMax).Take(pageMax).ToArray();
            return View("SearchResults", products);
        }
    }
}
