using Basket.Entities;
using E_Commerce.Basket.GrpcServices;
using E_Commerce.Basket.Repository;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace E_Commerce.Basket.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost(Name = "UpdateBasket")]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //: TODO : Communicate with Discount.gRPC
            // and Calculate latest prices of product into shopping cart
            //consume Discount gRPC
            var result = await _repository.UpdateBasket(basket);
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(result);
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<ActionResult> Delete(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok("Deleted");
        }
    }
}