using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions
{
    public abstract partial class BaseController : Controller
    {
        public string AppRootPath { get { return HttpContext.RequestServices?.GetService<IHostingEnvironment>().ContentRootPath; } }

        public string WebRootPath { get { return HttpContext.RequestServices?.GetService<IHostingEnvironment>().WebRootPath; } }

        public IConfiguration Configuration { get { return HttpContext.RequestServices?.GetService<IConfiguration>(); } }

        public  DefaultCookies Cookies { get { return HttpContext.RequestServices?.GetService< DefaultCookies>(); } }
         

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Prepare();
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Prepare();
            return base.OnActionExecutionAsync(context, next);
        }

        public virtual void Prepare()
        {
        }
    }
}
