namespace Pure.NetCoreExtensions.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    internal class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public HttpExceptionMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context).ConfigureAwait(false);
            }
            catch (HttpException httpException)
            {
                var factory = context.RequestServices.GetRequiredService<ILoggerFactory>();
                var logger = factory.CreateLogger<HttpExceptionMiddleware>();
                logger.LogInformation(
                    "Executing HttpExceptionMiddleware, setting HTTP status code {0}.",
                    httpException.StatusCode);

                context.Response.StatusCode = httpException.StatusCode;
                if (httpException != null)
                {
                    var responseFeature = context.Features.Get<IHttpResponseFeature>();
                    responseFeature.ReasonPhrase = httpException.Message;
                }
            }
        }
    }
}