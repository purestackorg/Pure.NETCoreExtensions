using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace Pure.NetCoreExtensions
{
    public static class EnvironmentExtensions
    {
        /// <summary>
        /// 启用全局静态化HostingEnvironment
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalHostingEnvironment(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
            GlobalHostEnvironment.Configure(httpContextAccessor);
            return app;
        }

        /// <summary>
        /// 启用全局静态化HostingEnvironment
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalHostingEnvironment(this IApplicationBuilder app, IHostingEnvironment env)
        { 
            GlobalHostEnvironment.Configure(env);
            return app;
        }
    }

}