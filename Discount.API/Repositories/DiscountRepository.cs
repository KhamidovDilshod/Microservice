using Dapper;
using E_Commerce.Discount.Entities;
using Npgsql;

namespace E_Commerce.Discount.Repositories;

/**@Author:Dilshodbek Hamidov @Date 08.07.2022*/
public class DiscountRepository : IDiscountRepository
{
    public DiscountRepository(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    private  IConfiguration Configuration { get; }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(Configuration.GetValue<string>("ConnectionStrings:Postgres"));
        var affected = await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName,Description,Amount) Values (@ProductName,@Description,@Amount)",
            new {coupon.ProductName, coupon.Description, coupon.Amount});
        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection =
            new NpgsqlConnection(Configuration.GetValue<string>("ConnectionStrings:Postgres"));
        var result = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@ProductName",
            new {ProductName = productName});
        return result >= 0;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(Configuration.GetValue<string>("ConnectionStrings:Postgres"));
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("Select * from Coupon WHERE ProductName=@ProductName", new {ProductName = productName});
        return coupon ?? new Coupon {ProductName = "No Discount", Amount = 0, Description = "No Discount description"};
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection =
            new NpgsqlConnection(Configuration.GetValue<string>("ConnectionStrings:Postgres"));

        var result = await connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName,Description=@Description,Amount=@Amount",
            new {coupon.ProductName, coupon.Description, coupon.Amount}
        );
        return coupon != null;
    }
}