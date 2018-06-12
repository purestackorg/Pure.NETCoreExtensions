using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pure.NetCoreExtensions.FriendlyExceptions.Options;
using System;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions.FriendlyExceptions
{
    internal class FriendlyExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<TranformOptions> _options;

        public FriendlyExceptionsMiddleware(RequestDelegate next,
            IOptions<TranformOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await context.HandleExceptionAsync(_options, exception);
            }
        }
    }
}