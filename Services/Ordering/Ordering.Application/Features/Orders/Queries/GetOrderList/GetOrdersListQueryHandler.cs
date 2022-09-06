using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
{
    public Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellation)
    {
        
    }
}