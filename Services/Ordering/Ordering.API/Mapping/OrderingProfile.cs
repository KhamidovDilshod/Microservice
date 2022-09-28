using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.EventBusConsumer;

namespace Ordering.Mapping;

public class OrderingProfile : Profile
{
    public OrderingProfile()
    {
        CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();     
    }
}