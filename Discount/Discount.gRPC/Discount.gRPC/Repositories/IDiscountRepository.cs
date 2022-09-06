using Discount.gRPC.Entities;

namespace Discount.gRPC.Repositories;

/**@Author:Dilshodbek Hamidov @Date 08.07.2022*/
public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}