﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = _orderRepository.GetById(request.Id);
        if (orderToDelete == null)
        {
            _logger.LogError("Order not exist on database");
            throw new NotFoundException(nameof(orderToDelete), "Order Id");
        }

        await _orderRepository.DeleteAsync(orderToDelete);
        _logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted");

        return Unit.Value;
    }
}