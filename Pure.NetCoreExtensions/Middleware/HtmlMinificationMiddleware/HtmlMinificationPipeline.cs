namespace Pure.NetCoreExtensions.Middleware
{
    using Microsoft.AspNetCore.Builder;
    public class HtmlMinificationPipeline
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseHTMLMinification();
        }
    }
}