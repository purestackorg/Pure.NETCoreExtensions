using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Pure.NetCoreExtensions.FriendlyExceptions.Options;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions.FriendlyExceptions
{
    public class FriendlyExceptionAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                var options = context.HttpContext.RequestServices.GetService(typeof(IOptions<TranformOptions>)) as IOptions<TranformOptions>;
                await context.HttpContext.HandleExceptionAsync(options, context.Exception);
                context.ExceptionHandled = true;
            }
        }
    }
}