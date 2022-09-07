using Basket.Entities;
using E_Commerce.Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

#pragma warning disable
namespace E_Commerce.Basket.Repository;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
    private DateTime _dateTime;

    private List<string> _keys = new();

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
    }

    public async Task DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));
        return await GetBasket(basket.Username);
    }

    public Task DailyCalculation()
    {
        if (_dateTime == DateTime.Today) return null;

        var overAll =
            _dateTime = DateTime.Today;
        return Task.FromCanceled(new CancellationToken(true));
    }

    public Task ClearDbPerHour()
    {
        return null;
    }
}