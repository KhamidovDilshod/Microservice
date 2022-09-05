namespace Basket.Entities
#pragma warning disable
{
    public class ShoppingCart
    {
        public string Username { get; set; }
        public string Date { get { return DateTime.Today.ToString(); } }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {

        }
        public ShoppingCart(string username)
        {
            username = username;
        }
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}