using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Pure.NetCoreExtensions.FriendlyExceptions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions.FriendlyExceptions
{
    internal static class HttpContextExtensions
    {
        internal static async Task HandleExceptionAsync(this Microsoft.AspNetCore.Http.HttpContext context,
            IOptions<TranformOptions> options,
            Exception exception)
        {
            var transformer = options.Value.Transforms?.FindTransform(exception);
            if (transformer == null)
                throw exception;

            var content = transformer.GetContent(exception);

            context.Response.ContentType = transformer.ContentType;
            context.Response.StatusCode = (int) transformer.StatusCode;
            context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = transformer.ReasonPhrase;
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(content);
            await context.Response.WriteAsync(content);
        }
    }
}