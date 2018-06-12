using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Pure.NetCoreExtensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly ServiceRegister ServiceRegister = new ServiceRegister();

        ///// <summary>
        ///// 启用全局静态化 ServiceProvider
        ///// </summary>
        ///// <param name="app"></param>
        ///// <returns></returns>
        //public static IApplicationBuilder UseGlobalServiceProvider(this IApplicationBuilder app)
        //{
        //    var svr = app.ApplicationServices.GetRequiredService<IServiceProvider>();
        //    GlobalServiceProvider.Configure(svr);
        //    return app;
        //}
        /// <summary>
        /// 启用全局静态化 ServiceProvider
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IServiceProvider UseGlobalServiceProvider(this IServiceProvider svr)
        { 
            GlobalServiceProvider.Configure(svr);
            return svr;
        }





        /// <summary>
        /// 根据继承标识ISingleton、IScoped、ITransient接口进行依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddAssembly(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            foreach (var assembly in assemblies)
            {
                ServiceRegister.RegisterAssembly(services, assembly);
            }
        }


        /// <summary>
        /// 根据条件注入
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to configure the MVC services.</param>
        /// <returns>The same services collection.</returns>
        public static IServiceCollection AddIf(
            this IServiceCollection services,
            bool condition,
            Func<IServiceCollection, IServiceCollection> action)
        {
            if (condition)
            {
                services = action(services);
            }

            return services;
        }

        /// <summary>
        /// 根据条件注入
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to configure the MVC services if the condition is <c>true</c>.</param>
        /// <param name="elseAction">The action used to configure the MVC services if the condition is <c>false</c>.</param>
        /// <returns>The same services collection.</returns>
        public static IServiceCollection AddIfElse(
            this IServiceCollection services,
            bool condition,
            Func<IServiceCollection, IServiceCollection> ifAction,
            Func<IServiceCollection, IServiceCollection> elseAction)
        {
            if (condition)
            {
                services = ifAction(services);
            }
            else
            {
                services = elseAction(services);
            }

            return services;
        }

    }
}
