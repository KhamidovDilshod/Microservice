using E_Commerce.Discount.Entities;

namespace E_Commerce.Discount.Repositories;

/**@Author:Dilshodbek Hamidov @Date 08.07.2022*/
public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}