﻿using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellation)
    {
        var orderList = await _orderRepository.GetOrderByUserName(request.UserName);
        return _mapper.Map<List<OrdersVm>>(orderList);
    }
}
