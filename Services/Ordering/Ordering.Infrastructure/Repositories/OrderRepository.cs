﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
    {
        if (DbContext.Orders == null) throw new NullReferenceException();
        var orderList = await DbContext.Orders
            .Where(o => o.UserName == userName)
            .ToListAsync();
        return orderList;

    }
}