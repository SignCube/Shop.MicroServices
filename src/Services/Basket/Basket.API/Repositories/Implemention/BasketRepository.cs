using Basket.API.Entities;
using Basket.API.Repositories.Abstraction;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories.Implemention
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasket(string username)
        {
            await _redisCache.RemoveAsync(username);
            
        }

        public async Task<ShoppingCart?> GetBasket(string username)
        {
            var basket =  await _redisCache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);

        }

        public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.Username,JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.Username);
        }
    }
}
