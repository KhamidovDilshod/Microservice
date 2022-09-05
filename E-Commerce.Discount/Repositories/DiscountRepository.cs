using Dapper;
using E_Commerce.Discount.Entities;
using Npgsql;

namespace E_Commerce.Discount.Repositories
{
    /**@Author:Dilshodbek Hamidov @Date 08.07.2022*/
    public class DiscountRepository : IDiscountRepository
    {
        private IConfiguration _configuration { get; }
        public DiscountRepository(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionStrings:Postgres"));
            var affected = await connection.ExecuteAsync("INSERT INTO Coupon (ProductName,Description,Amount) Values (@ProductName,@Description,@Amount)",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected == 0) return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionStrings:Postgres"));
            var result = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@ProductName", new { ProductName = productName });
            // throw new NotImplementedException();
            return result >= 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionStrings:Postgres"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("Select * from Coupon WHERE ProductName=@ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount description" };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionStrings:Postgres"));

            var result = await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@ProductName,Description=@Description,Amount=@Amount",
             new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount }
            );
            return coupon != null;
        }
    }
}