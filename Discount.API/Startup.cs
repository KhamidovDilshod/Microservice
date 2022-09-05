using E_Commerce.Discount.Repositories;

namespace E_Commerce.Discount
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            // });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllers();
              //   endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
              //   {
              //       Predicate = _ => true,
              //       ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
              //   });
          });
        }
    }
}