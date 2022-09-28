using Discount.gRPC.pros;
using E_Commerce.Basket.Entities;
using E_Commerce.Basket.GrpcServices;
using E_Commerce.Basket.Repository;
using MassTransit;
using MassTransit.MultiBus;
using OcelotApiGw.Middleware;

namespace E_Commerce.Basket;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        //General configuration
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<DiscountGrpcService>();
        services.AddAutoMapper(typeof(Startup));
        services.AddSwaggerGen();
        //Redis Configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetValue<string>("ConnectionStrings:Redis");
        });

        //gRPC Configuration
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            (o => o.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]));
        //Masstransit-RabbitMQ Configuration

        services.AddMassTransit(config =>
        {
            config.AddRequestClient<BasketCheckout>();

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.AddMiddleware();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}