using E_Commerce.Discount.Entities;
using E_Commerce.Discount.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        /**@Author:Dilshodbek Hamidov @Date 08.07.2022*/

        public IDiscountRepository _repository { get; }
        public DiscountController(IDiscountRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet("discount/{productName}")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var result = await _repository.GetDiscount(productName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("delete/{productName}")]
        public async Task<ActionResult<bool>> DeleteDiscount(string productName)
        {
            return await _repository.DeleteDiscount(productName);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateDiscount([FromBody] Coupon coupon)
        {
            return await _repository.CreateDiscount(coupon);
        }
        [HttpPost("update")]
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            return await _repository.UpdateDiscount(coupon);
        }

    }
}