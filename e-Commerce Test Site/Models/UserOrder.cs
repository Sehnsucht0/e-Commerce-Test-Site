namespace e_Commerce_Test_Site.Models
{
    public class UserOrder
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
