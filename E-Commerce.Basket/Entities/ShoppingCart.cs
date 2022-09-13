using Basket.Entities;

namespace E_Commerce.Basket.Entities;
#pragma warning disable

public class ShoppingCart
{
    public ShoppingCart()
    {
    }

    public ShoppingCart(string username)
    {
        username = username;
    }

    public string Username { get; set; }
    public string Date => DateTime.Today.ToString();
    public List<ShoppingCartItem> Items { get; set; } = new();

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            foreach (var item in Items) totalPrice += item.Price * item.Quantity;
            return totalPrice;
        }
    }
}