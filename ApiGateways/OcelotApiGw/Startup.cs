using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGw;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOcelot();
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        await app.UseOcelot();
    }
}