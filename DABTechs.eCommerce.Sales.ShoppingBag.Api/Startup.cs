using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DABTechs.eCommerce.Sales.Business.Interfaces;
using DABTechs.eCommerce.Sales.Business.Repositories;
using DABTechs.eCommerce.Sales.Providers.CosmosDb;

namespace DABTechs.eCommerce.Sales.ShoppingBag.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly string VipClient = "_vipclient";

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Load settings from json file and deserialise into classes
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .Build();

            services.AddScoped<IShoppingBagPersistence>((s) =>
            {
                return new ShoppingBagPersistence(config["CosmosDB:Endpoint"], config["CosmosDB:PrimaryKey"]);
            });

            services.AddScoped<IVipShoppingBagRepository, VipShoppingBagRepository>();

            // TODO better way to be had
            services.AddCors(options =>
            {
                options.AddPolicy(name: VipClient,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                    }
                    );
            });

            services.AddControllers();

            services.AddSwaggerGen(swagger => {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Vip ShoppingBag Api" });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors(VipClient);
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.DocumentTitle = "Vip ShoppingBag Api";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vip ShoppingBag Api");
                c.InjectJavascript("/swagger_custom.js");
            });

            app.UseRouting();


            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
