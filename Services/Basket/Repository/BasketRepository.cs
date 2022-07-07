using System.Timers;
using Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

#pragma warning disable
namespace Basket.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private DateTime dateTime;
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }
        private List<string> keys = new List<string>();

        public Task ClearDbPerHour()
        {
            return null;
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.Username);
        }

        public Task DailyCalculation()
        {
            if (this.dateTime == DateTime.Today)
            {
                return null;
            }
            else
            {
                var overAll =
                this.dateTime = DateTime.Today;
                return null;
            }
        }
    }
}