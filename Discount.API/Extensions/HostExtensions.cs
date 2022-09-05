using Npgsql;

namespace E_Commerce.Discount.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        var retryForAvailability = retry!.Value;

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();
        try
        {
            logger.LogInformation("Migrating Postgresql database ");
            using var connection =
                new NpgsqlConnection(configuration.GetValue<string>("ConnectionStrings:Postgres"));

            connection.Open();
            using var command = new NpgsqlCommand
            {
                Connection = connection
            };
            //Drops table
            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();
            //Create table
            command.CommandText =
                @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY , ProductName VARCHAR(100) NOT NULL,Description TEXT,AMOUNT INT)";
            command.ExecuteNonQuery();
            //Insert values to table
            command.CommandText =
                "INSERT INTO Coupon (ProductName,Description,Amount) VALUES ('Hp Victus 16,512SSD,16GB RAM','Hp Discount',1100);";
            command.ExecuteNonQuery();
            command.CommandText =
                "INSERT INTO Coupon (ProductName,Description,Amount) VALUES ('Macbook pro 16 32GB RAM,2TB','Apple  Discount',3000);";
            command.ExecuteNonQuery();
            logger.LogInformation("Migrated postgresql database.");
        }
        catch (NpgsqlException e)
        {
            logger.LogError(e, "An error occured while migrating the postgresql database");
            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, retryForAvailability);
            }
        }

        return host;
    }
}