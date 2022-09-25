using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed>? logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders()!);
            logger.LogInformation("Seed database  associated with context {DbContextName}", typeof(OrderContextSeed));
            await orderContext.SaveChangesAsync();
        }
    }

    private static IEnumerable<Order>? GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new()
            {
                UserName = "swn", FirstName = "Dilshod", LastName = "Hamidov",
                EmailAddress = "khamidovdilshodbek@gmail.com", AddressLine = "Tashkent,Uzbekistan", TotalPrice = 350
            }
        };
    }
}