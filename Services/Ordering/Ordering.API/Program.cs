using System.Reflection;
using EventBus.Messages.Common;
using FluentValidation.AspNetCore;
using MassTransit;
using OcelotApiGw.Middleware;
using Ordering.Application;
using Ordering.EventBusConsumer;
using Ordering.Extensions;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

#pragma warning disable CS0618

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

//General Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<BasketCheckoutConsumer>();

//Masstransit-RabbitMQ Configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BasketCheckoutConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("admin");
            h.Password("admin");
        });
        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue,
            action => { action.ConfigureConsumer<BasketCheckoutConsumer>(context); });
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();
app.MigrateDatabase<OrderContext>((context, service) =>
{
    var logger = service.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();