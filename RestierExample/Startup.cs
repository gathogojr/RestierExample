using System.Linq;
using RestierExample.Controllers;
using RestierExample.Data;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Restier.AspNetCore;
using Microsoft.Restier.Core;

namespace RestierExample
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRestier(builder => builder.AddRestierApi<RestierExampleApi>(restierServices =>
            {
                restierServices.AddEFCoreProviderServices<RestierExampleDbContext>((serviceProvider, dbContextOptions) =>
                {
                    dbContextOptions.UseSqlServer(Configuration.GetConnectionString("LabExDbConnectionString"));
                })
                .AddSingleton(new ODataValidationSettings
                {
                    MaxTop = 5,
                    MaxAnyAllExpressionDepth = 3,
                    MaxExpansionDepth = 3,
                });
            }));
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter().OrderBy().Count().Expand().MaxTop(null);
                routeBuilder.MapRestier(restierRouteBuilder =>
                {
                    restierRouteBuilder.MapApiRoute<RestierExampleApi>("odata", "odata", true);
                });
            });
        }
    }
}
