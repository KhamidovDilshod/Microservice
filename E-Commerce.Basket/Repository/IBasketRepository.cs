using Basket.Entities;

namespace E_Commerce.Basket.Repository;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
    Task DeleteBasket(string userName);
    Task DailyCalculation();
}