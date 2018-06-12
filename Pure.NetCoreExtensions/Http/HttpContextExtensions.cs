using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
namespace Pure.NetCoreExtensions
{
    public static class HttpContextExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// 启用全局静态化HttpContext
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            GlobalHttpContext.Configure(httpContextAccessor);
            return app;
        }


        #region 拓展方法
        private const string NoCache = "no-cache";
        private const string NoCacheMaxAge = "no-cache,max-age=";
        private const string NoStore = "no-store";
        private const string NoStoreNoCache = "no-store,no-cache";
        private const string PublicMaxAge = "public,max-age=";
        private const string PrivateMaxAge = "private,max-age=";

        /// <summary>
        /// Adds the Cache-Control and Pragma HTTP headers by applying the specified cache profile to the HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="cacheProfile">The cache profile.</param>
        /// <returns>The HTTP context</returns>
        /// <exception cref="System.ArgumentNullException">context or cacheProfile.</exception>
        public static Microsoft.AspNetCore.Http.HttpContext ApplyCacheProfile(this Microsoft.AspNetCore.Http.HttpContext context, CacheProfile cacheProfile)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (cacheProfile == null)
            {
                throw new ArgumentNullException(nameof(cacheProfile));
            }

            var headers = context.Response.Headers;

            if (!string.IsNullOrEmpty(cacheProfile.VaryByHeader))
            {
                headers[HeaderNames.Vary] = cacheProfile.VaryByHeader;
            }

            if (cacheProfile.NoStore == true)
            {
                // Cache-control: no-store, no-cache is valid.
                if (cacheProfile.Location == ResponseCacheLocation.None)
                {
                    headers[HeaderNames.CacheControl] = NoStoreNoCache;
                    headers[HeaderNames.Pragma] = NoCache;
                }
                else
                {
                    headers[HeaderNames.CacheControl] = NoStore;
                }
            }
            else
            {
                string cacheControlValue = null;
                var duration = cacheProfile.Duration.GetValueOrDefault().ToString(CultureInfo.InvariantCulture);
                switch (cacheProfile.Location)
                {
                    case ResponseCacheLocation.Any:
                        cacheControlValue = PublicMaxAge + duration;
                        break;
                    case ResponseCacheLocation.Client:
                        cacheControlValue = PrivateMaxAge + duration;
                        break;
                    case ResponseCacheLocation.None:
                        cacheControlValue = NoCacheMaxAge + duration;
                        headers[HeaderNames.Pragma] = NoCache;
                        break;
                    default:
                        var exception = new NotImplementedException($"Unknown {nameof(ResponseCacheLocation)}: {cacheProfile.Location}");
                        Debug.Fail(exception.ToString());
                        throw exception;
                }

                headers[HeaderNames.CacheControl] = cacheControlValue;
            }

            return context;
        }
        public static Uri GetUri(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.Scheme))
                throw new ArgumentException("Http request Scheme is not specified");
            if (!request.Host.HasValue)
                throw new ArgumentException("Http request Host is not specified");
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(request.Scheme).Append("://").Append((object)request.Host);
            if (request.Path.HasValue)
                stringBuilder.Append(request.Path.Value);
            if (request.QueryString.HasValue)
                stringBuilder.Append((object)request.QueryString);
            return new Uri(stringBuilder.ToString());
        }

        public static string GetAbsoluteUri(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return new StringBuilder()
                .Append(request.Scheme)
                .Append("://")
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }
        public static IHeaderDictionary GetServerVariables(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return request.Headers;
        }
        public static string GetUserIp(this Microsoft.AspNetCore.Http.HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        #endregion


    }

}