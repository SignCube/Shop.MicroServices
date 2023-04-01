using Dapper;
using Discount.API.Entities;
using Discount.API.Repositories.Abstraction;
using Npgsql;

namespace Discount.API.Repositories.Implemention
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await
                connection.ExecuteAsync("INSERT into Coupon ( ProductName , Description , Amount) Values ( @ProductName,@Description,@Amount);", new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount
                });

            if (affected > 0) return true;
            return false;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await
                connection.ExecuteAsync(
                    "delete from Coupon where ProductName  =@ProductName"
                    , new
                    {
                        ProductName = productName

                    });
            if (affected > 0) return true;
            return false;

        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("select * from Coupon where ProductName = @ProductName ", new { ProductName = productName });
            if (coupon != null)
                return coupon;
            return new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };

        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await
                connection.ExecuteAsync("UPDATE Coupon SET ProductName = @ProductName, Description = @Description , Amount = @Amount where ID=@Id", new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    Id = coupon.Id
                });
            if (affected > 0) return true;
            return false;
        }
    }
}
