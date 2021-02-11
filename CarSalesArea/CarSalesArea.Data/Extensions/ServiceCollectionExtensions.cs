using CarSalesArea.Data.Context;
using CarSalesArea.Data.Models;
using CarSalesArea.Data.Repositories;
using CarSalesArea.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarSalesArea.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IManagerRepository, ManagerRepository>();
            services.AddTransient<ISalesAreaRepository, SalesAreaRepository>();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddDbContext<CarSalesAreaDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("CarSalesAreaConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CarSalesAreaDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
