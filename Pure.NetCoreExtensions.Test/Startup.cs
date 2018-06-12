using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pure.NetCoreExtensions;

namespace Pure.NetCoreExtensions.Test
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=HelloWorld}/{action=Index}/{id?}");
            });
            app.UseCors("").
                UseExceptionHandler()
                .UseGlobalErrorHandling();

            //global middleware
            app.UseGlobalHostingEnvironment(env)
                .UseGlobalHttpContext()
                .UseGlobalLoggerFactory()
                .UseGlobalErrorHandling()
               ; 

            app.UseGlobalHttpContext();

            app.UseGlobalLoggerFactory();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
