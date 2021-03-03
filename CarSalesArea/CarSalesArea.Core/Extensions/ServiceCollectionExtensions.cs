﻿using CarSalesArea.Core.Services;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarSalesArea.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<ISalesAreaService, SalesAreaService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IMediaService, MediaService>();

            return services;
        }
    }
}
