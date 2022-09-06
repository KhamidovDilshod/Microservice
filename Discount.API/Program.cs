using E_Commerce.Discount.Extensions;

namespace E_Commerce.Discount;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.MigrateDatabase<Program>();
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}