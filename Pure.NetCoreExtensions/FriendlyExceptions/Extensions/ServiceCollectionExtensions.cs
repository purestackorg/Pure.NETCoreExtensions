using Microsoft.Extensions.DependencyInjection;
using Pure.NetCoreExtensions.FriendlyExceptions.Options;
using System;

namespace Pure.NetCoreExtensions.FriendlyExceptions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddFriendlyExceptionsTransforms(this IServiceCollection services,
            Action<TranformOptions> setupAction)
        {
            services.Configure(setupAction);
        }
    }
}