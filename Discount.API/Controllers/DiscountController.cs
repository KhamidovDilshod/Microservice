using E_Commerce.Discount.Entities;
using E_Commerce.Discount.Repositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

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
        var result = await Repository.DeleteDiscount(productName);
        return Ok(new CommonMessage(message: $"Deleted :{result}", id: 0));
    }

    [HttpPost]
    public async Task<ActionResult> CreateDiscount([FromBody] Coupon coupon)
    {
        bool result = await Repository.CreateDiscount(coupon);
        return Ok(new CommonMessage(id: coupon.Id, message: result.ToString()));
    }

    [HttpPost("update")]
    public async Task<ActionResult> UpdateDiscount(Coupon coupon)
    {
        bool result = await Repository.UpdateDiscount(coupon);
        return Ok(new CommonMessage(id: coupon.Id, message: result.ToString()));
    }
}