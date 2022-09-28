using System.Net;
using AutoMapper;
using E_Commerce.Basket.Entities;
using E_Commerce.Basket.GrpcServices;
using E_Commerce.Basket.Repository;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable
namespace E_Commerce.Basket.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IBasketRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _endpoint;

    public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService, IMapper mapper,
        IPublishEndpoint endpoint)
    {
        _repository = repository ?? throw new ArgumentException(nameof(repository));
        _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        _mapper = mapper;
        _endpoint = endpoint;
    }

    [HttpGet("{username}", Name = "GetBasket")]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
    {
        var basket = await _repository.GetBasket(username);
        return Ok(basket ?? new ShoppingCart(username));
    }

    [HttpPost(Name = "UpdateBasket")]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        // and Calculate latest prices of product into shopping cart
        //consume Discount gRPC
        foreach (var item in basket.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }

        var result = await _repository.UpdateBasket(basket);

        return Ok(result);
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]
    public async Task<ActionResult> Delete(string userName)
    {
        await _repository.DeleteBasket(userName);
        return Ok("Deleted");
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Accepted)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        //get existing basket with total price
        var basket = await _repository.GetBasket(basketCheckout.UserName);
        if (basket == null)
        {
            return BadRequest();
        }

        //send checkout event to rabbitmq
        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        eventMessage.TotalPrice = basket.TotalPrice;
       await _endpoint.Publish(eventMessage);
        //Create basketCheckoutEvent

        //remove the basket
        await _repository.DeleteBasket(basket.Username);
        return Accepted();
    }
}