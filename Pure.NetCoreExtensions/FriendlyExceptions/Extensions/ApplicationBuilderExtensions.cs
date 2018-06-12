using Microsoft.AspNetCore.Builder;

namespace Pure.NetCoreExtensions.FriendlyExceptions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseFriendlyExceptions(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<FriendlyExceptionsMiddleware>();
        }
    }
}