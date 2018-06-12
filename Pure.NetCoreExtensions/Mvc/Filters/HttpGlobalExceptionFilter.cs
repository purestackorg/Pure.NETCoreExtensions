using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Pure.NetCoreExtensions
{
    /// <summary>
    /// 错误处理类
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        public static ILogger Log = null;//日志记录

        public HttpGlobalExceptionFilter(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this._env = env;
            Log = loggerFactory.CreateLogger<HttpGlobalExceptionFilter>();
        }

        public ContentResult FailedMsg(string msg = null)
        {
            string retResult = "{\"status\":1,\"msg\":\"" + msg + "\"}";//, msg);
             
            return new ContentResult() { Content = retResult };
        }
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            //执行过程出现未处理异常
            Exception ex = filterContext.Exception;
#if DEBUG
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string msg = null;

                if (ex is Exception)
                {
                    msg = ex.Message;
                    filterContext.Result = this.FailedMsg(msg);
                    filterContext.ExceptionHandled = true;
                    return;
                }
            }

            this.LogException(filterContext);
            return;
#endif
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string msg = null;

                if (ex is Exception)
                {
                    msg = ex.Message;
                }
                else
                {
                    this.LogException(filterContext);
                    msg = "服务器错误";
                }

                filterContext.Result = this.FailedMsg(msg);
                filterContext.ExceptionHandled = true;
                return;
            }
            else
            {
                //对于非 ajax 请求
                this.LogException(filterContext);
                return;
            }
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="filterContext"></param>
        private void LogException(ExceptionContext filterContext)
        {
            string mid = filterContext.HttpContext.Request.Query["mid"];//codding 后续完善每个action带一个id
            var areaName = (filterContext.RouteData.DataTokens["area"] == null ? "" : filterContext.RouteData.DataTokens["area"]).ToString().ToLower();
            var controllerName = (filterContext.RouteData.Values["controller"]).ToString().ToLower();
            var actionName = (filterContext.RouteData.Values["action"]).ToString().ToLower();

            #region --记录日志 codding 后续增加自定义字段的日志。如：记录Controller/action，模块ID等--
            Log.LogError(filterContext.Exception, "全局错误：areaName：" + areaName + ",controllerName：" + controllerName + ",action：" + actionName);
            #endregion
        }
    }
}
