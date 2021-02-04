 using AutoMapper;
 using CarSalesArea.Core.Extensions;
 using CarSalesArea.Core.Infrastructure;
 using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarSalesArea.Data.Extensions;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Mvc.Versioning;

 namespace CarSalesArea.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options=>
                {
                    options.CacheProfiles.Add("Static", new CacheProfile
                    {
                        Duration = 36000
                    });
                });
            services.AddCore(Configuration);
            services.AddData(Configuration);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector =
                    new CurrentImplementationApiVersionSelector(options);
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument();
            services.AddAutoMapper(
                options
                    => options.AddProfile<MappingProfile>());
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseHsts();
            }

            app.UseResponseCaching();

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
