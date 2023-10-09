using e_Commerce_Test_Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_Commerce_Test_Site.Components
{
    public class CategorySidebar: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
         {
             string[] categories = await APIGetter.GetCategoriesAsync();
             return View(categories);
         } 
    }
}