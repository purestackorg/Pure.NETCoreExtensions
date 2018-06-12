using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pure.NetCoreExtensions
{
    public static class DefaultIdentityUserExtensions
    {
        public static IServiceCollection AddDefaultUser<TUser, TKey>(this IServiceCollection self)
            where TUser : IdentityUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return self.AddScoped<DefaultIdentityUser<TUser, TKey>>();
        }
    }

}