﻿namespace e_Commerce_Test_Site.Models
{
    public class JsonProduct
    {
        public Product[]? Products { get; set; }

        public int Total { get; set; }

        public int Skip { get; set; }

        public int Limit { get; set; }
    }
}