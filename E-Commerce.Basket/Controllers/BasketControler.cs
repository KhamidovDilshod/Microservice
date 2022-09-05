using Basket.Entities;
using Basket.Repository;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace Basket.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
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
            return Ok(await _repository.UpdateBasket(basket));
        }
        [HttpDelete("{userName}",Name ="DeleteBasket")]
        public async Task<ActionResult> Delete(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok("Deleted");
        }

    }
}