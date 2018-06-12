using Microsoft.AspNetCore.Hosting;
using System;

namespace Pure.NetCoreExtensions
{

    /// <summary>
    /// <see cref="IWebHostBuilder"/> extension methods.
    /// </summary>
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// 根据条件构造WebHostBuilder
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the host builder.</param>
        /// <returns>The same host builder.</returns>
        public static IWebHostBuilder UseIf(
            this IWebHostBuilder hostBuilder,
            bool condition,
            Func<IWebHostBuilder, IWebHostBuilder> action)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                hostBuilder = action(hostBuilder);
            }

            return hostBuilder;
        }

        
        public static IWebHostBuilder UseIfElse(
            this IWebHostBuilder hostBuilder,
            bool condition,
            Func<IWebHostBuilder, IWebHostBuilder> ifAction,
            Func<IWebHostBuilder, IWebHostBuilder> elseAction)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition)
            {
                hostBuilder = ifAction(hostBuilder);
            }
            else
            {
                hostBuilder = elseAction(hostBuilder);
            }

            return hostBuilder;
        }
    }
}
