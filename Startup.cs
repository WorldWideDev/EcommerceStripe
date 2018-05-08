using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using ECommerce.Models;
using Stripe;
using ECommerce.Configurations;

namespace ECommerce
{
    public class Startup
    {
        private IConfiguration Configuration {get;set;}
        public Startup (IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.AddDbContext<MainContext>(options => options.UseNpgsql(Configuration["DBInfo:ConnectionString"]));
            // Add framework services.
            services.AddRouting(options => options.LowercaseUrls=true);
            services.AddMvc();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                app.UseDeveloperExceptionPage();
            }
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes => {
                routes.MapRoute(name:"default", template:"{controller=Customer}/{action=Index}/{id?}");
                routes.MapRoute(name:"products", template:"{controller=Product}/{action=Index}/{id?}");
                routes.MapRoute(name:"orders", template:"{controller=Order}/{action=Index}/{id?}");
            });
        }
    }
}
