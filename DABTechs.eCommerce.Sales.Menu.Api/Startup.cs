using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DABTechs.eCommerce.Sales.Common;
using DABTechs.eCommerce.Sales.Providers.Menu;

namespace DABTechs.eCommerce.Sales.Menu.Api
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

            // TODO RequireHttpsMetadat needs to  be refactored for production.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = config["AppSettings:Authority"];
                    options.Audience = SecurityResources.VipMenuApiResource;
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuer = true,
                    //    ValidateAudience = true,
                    //    ValidateLifetime = true,
                    //    ValidateIssuerSigningKey = true,
                    //    ValidIssuer = config["AppSettings:Issuer"],
                    //    ValidAudience = config["AppSettings:Audience"],
                    //    IssuerSigningKey = JwtSecurityKey.Create(SecurityScopes.VipApiSecret)
                    //};
                });

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
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Vip Menu Api" });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            // TODO this is to get the Mega Nav xml file need to remove in new solution
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

            services.AddScoped<IVipMenu, VipMenu>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(VipClient);
            app.UseStaticFiles();

            

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.DocumentTitle = "Vip Search Api";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vip Search Api");
                c.InjectJavascript("/swagger_custom.js");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
