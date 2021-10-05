using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SparePartsOrders.BLL.Contracts;
using SparePartsOrders.BLL.Services;
using SparePartsOrders.DAL.Contracts;
using SparePartsOrders.DAL.Repositories;
using SparePartsOrders.DAL.Settings;
using System;

namespace SparePartsOrders.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<OrderCollectionSettings>(
                Configuration.GetSection(nameof(OrderCollectionSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<OrderCollectionSettings>>().Value);
          
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
          
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
