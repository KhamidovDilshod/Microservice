using AutoMapper;
using E_Commerce.Basket.Entities;
using EventBus.Messages.Events;

namespace E_Commerce.Basket.Mapper;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}