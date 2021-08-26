using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DABTechs.eCommerce.Sales.Identity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string VipClient = "_vipclient";

        // TODO if you get password prompt from google it says localhost:5500 rather than
        // identity.sales.next.co.uk
        public Startup(IWebHostEnvironment env)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
               .Build();

            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddControllersWithViews();

            // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;              
                options.EmitStaticAudienceClaim = true;
                options.IssuerUri = "http://identity.sale.dab.localhost";
            });

            builder.AddInMemoryApiResources(Config.GetApiResources());
            builder.AddInMemoryApiScopes(Config.GetApiScopes());
            builder.AddInMemoryIdentityResources(Config.GetIdentityResources());         
            builder.AddInMemoryClients(Config.GetClients(this.Configuration));

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
            builder.AddTestUsers(TestUsers.Users);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // TODO this is a work aroung for http and Identity server needs to be removed
            // or investigated or use https
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax});

            app.UseCors(VipClient);

            app.UseStaticFiles();

            app.UseRouting();

            var forwaredHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };

            forwaredHeaderOptions.ForwardLimit = 2; // may need this is config
            forwaredHeaderOptions.KnownNetworks.Clear();
            forwaredHeaderOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwaredHeaderOptions);        

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
