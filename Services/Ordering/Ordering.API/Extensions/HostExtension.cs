﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Extensions;

public static class HostExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host,
        Action<TContext, IServiceProvider> seeder,
        int? retry = 0) where TContext : DbContext
    {
        int retryForAvailability = retry!.Value;
        using var scope = host.Services.CreateScope();
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}",
                    typeof(TContext).Name);
                InvokeSeeder(seeder!, context, services);


                logger.LogInformation("Migrated database associated with context {DbContextName}",
                    typeof(TContext).Name);
            }
            catch (SqlException ex)
            {
                logger.LogInformation("An error occured while migrating the database used on context :{DbContextName}",
                    typeof(TContext).Name);
                if (retryForAvailability >= 3) throw;
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, seeder, retryForAvailability);
            }
        }
        return host;
    }

    private static void InvokeSeeder<TContext>(
        Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services) where TContext : DbContext?
    {
        context?.Database.Migrate();
        seeder(context, services);
    }
}