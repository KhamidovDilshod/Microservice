using Basket.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

#pragma warning disable
namespace E_Commerce.Basket.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private DateTime _dateTime;
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        private List<string> _keys = new List<string>();

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
            return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.Username);
        }

        public Task DailyCalculation()
        {
            if (this._dateTime == DateTime.Today)
            {
                return null;
            }

            var overAll =
                this._dateTime = DateTime.Today;
            return Task.FromCanceled(new CancellationToken(true));
        }
    }
}