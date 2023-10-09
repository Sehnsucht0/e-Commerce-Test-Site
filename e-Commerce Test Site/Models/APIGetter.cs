using e_Commerce_Test_Site.Models.DTOs;

namespace e_Commerce_Test_Site.Models
{
    public static class APIGetter
    {
        private static HttpClient SharedClient { get; } = new HttpClient() { BaseAddress = new Uri("https://dummyjson.com/") };

        public static List<Product>? StoreCartOrder { get; set; }

        public static List<SessionCartDTO>? StoreCartQuantities { get; set; }

        public static async Task<string[]> GetCategoriesAsync()
        {
            string[] categories = await SharedClient.GetFromJsonAsync<string[]>("products/categories") ?? Array.Empty<string>();
            return categories;
        }

        public static async Task<Product[]> GetAllProducts()
        {
            JsonProduct jsonProduct = await SharedClient.GetFromJsonAsync<JsonProduct>("products") ?? new JsonProduct();
            var products = jsonProduct.Products ?? Array.Empty<Product>();
            return products;
        }

        public static async Task<Product[]> GetSearchResultsAsync(string name)
        {
            JsonProduct jsonProduct = await SharedClient.GetFromJsonAsync<JsonProduct>("products/search?q=" +  name) ?? new JsonProduct();
            var products = jsonProduct.Products ?? Array.Empty<Product>();
            return products;
        }

        public static async Task<Product[]> GetCategoryProductsAsync(string category)
        {
            JsonProduct jsonProduct = await SharedClient.GetFromJsonAsync<JsonProduct>("products/category/" + category) ?? new JsonProduct();
            var products = jsonProduct.Products ?? Array.Empty<Product>();
            return products;
        }

        public static Product[] FilterCategory(Product[] products, string category)
        {
            List<Product> filteredProducts = new List<Product>();
            foreach (Product product in products)
            {
                if(product.Category == category) filteredProducts.Add(product);
            }
            return filteredProducts.ToArray();
        }

        public static async Task<Product> GetProductById(int id)
        {
            Product product = await SharedClient.GetFromJsonAsync<Product>("products/" + id) ?? new Product();
            return product;
        }
    }
}
