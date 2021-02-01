using System;
using System.Collections.Generic;
using System.Text;
using CarSalesArea.Data.Repositories;
using CarSalesArea.Data.Repositories.Interfaces;
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

            return services;
        }
    }
}
