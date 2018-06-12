namespace Pure.NetCoreExtensions.Middleware
{
    using Microsoft.AspNetCore.Builder;

    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseHTMLMinification(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HtmlMinificationMiddleware>();
        }
        public static IApplicationBuilder UseHTMLMinification(this IApplicationBuilder app, 
            string excludeFilter)
        {
            var options = new HtmlMinificationOptions() { ExcludeFilter = excludeFilter  };
            return app.UseMiddleware<HtmlMinificationMiddleware>(options);
        }
        public static IApplicationBuilder UseHTMLMinification(this IApplicationBuilder app, 
            HtmlMinificationOptions minificationOptions)
        {
            return app.UseMiddleware<HtmlMinificationMiddleware>(minificationOptions);
        }
    }
}