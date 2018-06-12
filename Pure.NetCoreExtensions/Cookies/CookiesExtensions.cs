using Microsoft.Extensions.DependencyInjection;
namespace Pure.NetCoreExtensions
{
    public static class CookiesExtensions
    {
        public static IServiceCollection AddDefaultCookies(this IServiceCollection self)
        {
            return self.AddScoped<DefaultCookies>();
        }
    }

}

