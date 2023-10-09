namespace e_Commerce_Test_Site.Models
{
    public class CartItems
    {
        public int Id { get; set; }
        public int UserDataId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public UserData? UserData { get; set; }
    }
}
