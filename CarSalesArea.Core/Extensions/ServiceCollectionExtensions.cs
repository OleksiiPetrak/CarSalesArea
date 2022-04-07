using CarSalesArea.Core.Models;
using CarSalesArea.Core.Services;
using CarSalesArea.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

            // Wire up a single instance of BlobStorage, calling Initialize() when we first use it.
            services.AddSingleton<IStorageService>(serviceProvider => {
                var blobStorage = new StorageService(serviceProvider.GetService<IOptions<AzureStorageConfig>>());
                blobStorage.Initialize().GetAwaiter().GetResult();
                return blobStorage;
            });

            return services;
        }
    }
}
