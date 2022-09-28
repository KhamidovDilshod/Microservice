using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.EventBusConsumer;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public BasketCheckoutConsumer(IMapper mapper, ILogger<BasketCheckoutConsumer> logger, IMediator mediator)
    {
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var result = await _mediator.Send(command);

        _logger.LogInformation("Basket Checkout: Event consumed successfully . Created Order Id:{NewOrderId}", result);
    }
}