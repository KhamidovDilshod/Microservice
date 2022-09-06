using Discount.gRPC.pros;
using E_Commerce.Basket.GrpcServices;
using E_Commerce.Basket.Repository;

namespace E_Commerce.Basket
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<DiscountGrpcService>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("ConnectionStrings:Redis");
            });
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
                (o => o.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}