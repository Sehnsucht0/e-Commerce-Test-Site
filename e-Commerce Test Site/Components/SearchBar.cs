﻿using Microsoft.AspNetCore.Mvc;
using e_Commerce_Test_Site.Models;

namespace e_Commerce_Test_Site.Components
{
    public class SearchBar : ViewComponent
    {
        public IViewComponentResult Invoke() => View(new SearchQuery());
    }
}
