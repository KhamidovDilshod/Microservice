﻿using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    public OrderContext()
    {
    }


    public DbSet<Order>? Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedByDate = DateTime.Now;
                    entry.Entity.CreatedBy = "swn";
                    entry.Entity.LastModifiedBy = "swn";
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.CreatedByDate = DateTime.Now;
                    entry.Entity.CreatedBy = "swn";
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "swn";
                    break;
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}