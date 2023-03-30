using Basket.API.Entities;

namespace Basket.API.Repositories.Abstraction
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetBasket(string username);
        Task<ShoppingCart?> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string username);

    }
}
