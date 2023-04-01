using Discount.API.Entities;

namespace Discount.API.Repositories.Abstraction
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);   
        Task<bool> DeleteDiscount(string productName);


    }
}
