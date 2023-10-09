namespace e_Commerce_Test_Site.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public int Count { get; set; }
        public UserData? UserData { get; set; }
        public ICollection<UserOrder>? UserOrders { get; set; }
    }
}
