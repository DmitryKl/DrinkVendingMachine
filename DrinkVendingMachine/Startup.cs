using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DrinkVendingMachine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using DrinkVendingMachine.Infrastructure;

namespace DrinkVendingMachine
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {            
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            connectionString = connectionString.Replace("%CONTENTROOTPATH%", env.ContentRootPath);
            services.AddDbContext<DrinkContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IDrinkRepository, EFDrinkRepository>();
            services.AddTransient<ICoinRepository, EFCoinRepository>();
            services.AddMvc();
            services.AddScoped<CustomAuthorizationAttribute>(x => 
                new CustomAuthorizationAttribute((string)Configuration.GetValue(typeof(string), "Key")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            });

        }
    }
}
