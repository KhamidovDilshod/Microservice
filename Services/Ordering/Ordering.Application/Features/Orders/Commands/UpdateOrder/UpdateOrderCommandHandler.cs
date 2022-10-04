using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable
namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
        ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate =  _orderRepository.GetById(request.Id);
        if (orderToUpdate is null)
        {
            _logger.LogError("Order not exist on database");
            return Unit.Value;
        }
        _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
        await _orderRepository.UpdateAsync(orderToUpdate!);
        _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");
        return Unit.Value;
    }
}