using Microsoft.AspNetCore.Mvc;
using e_Commerce_Test_Site.Models.DTOs;

namespace e_Commerce_Test_Site.Components
{
    public class Paging: ViewComponent
    {
        public IViewComponentResult Invoke(int page, int pagetotal, string pagingquery) => View(new PagingDTO() { Page = page, PageTotal = pagetotal, PagingQuery = pagingquery});
    }
}
