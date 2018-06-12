 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Pure.NetCoreExtensions.Middleware;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
namespace Pure.NetCoreExtensions
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> extension methods.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {

        public static void UseGlobalErrorHandling(this IApplicationBuilder app )
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException)
                    {
                        throw;
                    }

                    var traceId = context.Request.GetRequestId();
                    var errorFormat = "Unhandled exception processing traceId: {traceId}";
                    var errorLogger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("ApplicationBuilderExtensions");
                    errorLogger.LogError(ex, errorFormat, traceId);

                    if (!context.Response.HasStarted)
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync($"{{ \"message\": \"Internal Error\", \"traceId\": \"{traceId}\" }}");
                    }
                }
            });
        }

        public static void UseForwardedServerInfo(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var request = context.Request;

                var scheme = request.Headers.FirstOrDefault("X-Forwarded-Proto");
                if (scheme != null)
                {
                    request.Scheme = scheme;
                }

                var host = request.Headers.FirstOrDefault("X-Forwarded-Host");
                if (host != null)
                {
                    request.Host = new HostString(host);
                }

                var prefix = request.Headers.FirstOrDefault("X-Forwarded-Prefix");
                if (prefix != null)
                {
                    request.PathBase = prefix;
                }

                var pathBase = request.Headers.FirstOrDefault("X-Forwarded-Path");
                if (pathBase != null)
                {
                    request.PathBase = pathBase;
                }

                await next.Invoke();
            });
        }

        public static void UseRealRemoteIp(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var realIp = context.Request.Headers.FirstOrDefault("X-Real-IP");
                if (realIp != null)
                {
                    context.Connection.RemoteIpAddress = System.Net.IPAddress.Parse(realIp);
                }

                await next.Invoke();
            });
        }
        /// <summary>
        /// Allows the use of <see cref="HttpException"/> as an alternative method of returning an error result.
        /// </summary>
        /// <param name="application">The application builder.</param>
        /// <returns>The same application builder.</returns>
        public static IApplicationBuilder UseHttpException(this IApplicationBuilder application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return application.UseMiddleware<HttpExceptionMiddleware>();
        }
 
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                application = action(application);
            }

            return application;
        }

        
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            bool condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition)
            {
                application = ifAction(application);
            }
            else
            {
                application = elseAction(application);
            }

            return application;
        }

        
        public static IApplicationBuilder UseIf(
            this IApplicationBuilder application,
            Func<Microsoft.AspNetCore.Http.HttpContext, bool> condition,
            Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var builder = application.New();

            action(builder);

            return application.Use(next =>
            {
                builder.Run(next);

                var branch = builder.Build();

                return context =>
                {
                    if (condition(context))
                    {
                        return branch(context);
                    }

                    return next(context);
                };
            });
        }

         
        public static IApplicationBuilder UseIfElse(
            this IApplicationBuilder application,
            Func<Microsoft.AspNetCore.Http.HttpContext, bool> condition,
            Func<IApplicationBuilder, IApplicationBuilder> ifAction,
            Func<IApplicationBuilder, IApplicationBuilder> elseAction)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (ifAction == null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction == null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            var ifBuilder = application.New();
            var elseBuilder = application.New();

            ifAction(ifBuilder);
            elseAction(elseBuilder);

            return application.Use(next =>
            {
                ifBuilder.Run(next);
                elseBuilder.Run(next);

                var ifBranch = ifBuilder.Build();
                var elseBranch = elseBuilder.Build();

                return context =>
                {
                    if (condition(context))
                    {
                        return ifBranch(context);
                    }
                    else
                    {
                        return elseBranch(context);
                    }
                };
            });
        }

       
        public static IApplicationBuilder UseInternalServerErrorOnException(this IApplicationBuilder application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return application.UseMiddleware<InternalServerErrorOnExceptionMiddleware>();
        }
 
        public static IApplicationBuilder UseNoServerHttpHeader(this IApplicationBuilder application)
        {
            if (application == null)
            {
                throw new ArgumentNullException(nameof(application));
            }

            return application.UseMiddleware<NoServerHttpHeaderMiddleware>();
        }
    }
}
