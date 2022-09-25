using E_Commerce.Discount.Entities;
using E_Commerce.Discount.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Discount.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    public DiscountController(IDiscountRepository repository)
    {
        Repository = repository;
    }

    /**@Author:Dilshodbek Hamidov @Date 08.07.2022*/

    public IDiscountRepository Repository { get; }

    [HttpGet("discount/{productName}")]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var result = await Repository.GetDiscount(productName);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("delete/{productName}")]
    public async Task<ActionResult<bool>> DeleteDiscount(string productName)
    {
        return await Repository.DeleteDiscount(productName);
    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateDiscount([FromBody] Coupon coupon)
    {
        return await Repository.CreateDiscount(coupon);
    }

    [HttpPost("update")]
    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        return await Repository.UpdateDiscount(coupon);
    }
}