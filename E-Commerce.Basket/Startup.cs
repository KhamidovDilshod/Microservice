using E_Commerce.Basket.Repository;

namespace E_Commerce.Basket
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
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _configuration.GetValue<string>("ConnectionStrings:Redis");
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllers();
          });
        }
    }
}