# Pure.NETCoreExtentensions
NET Core Extentsion methods(support NET Core2.0+)

Include Extensions methods:
    DistributedCacheExtensions
    ConfigurationExtensions
    CookiesExtensions
    ServiceCollectionExtensions
    EnvironmentExtensions
    HttpContextExtensions
    HttpRequestExtentions
    FormFileExtentions
    HeaderDictionaryExtensions
    DefaultIdentityUserClaimsExtensions
    LoggerFactoryExtensions
    UrlHelperExtensions
    SmtpEmailSenderExtensions
    WebHostBuilderExtensions
    ApplicationBuilderExtensions

Include Middlewares:
    FriendlyExceptionsMiddleware
    HtmlMinificationMiddleware
    HttpExceptionMiddleware
    InternalServerErrorOnExceptionMiddleware
    NoServerHttpHeaderMiddleware
    ClientRateLimitMiddleware
    IpRateLimitMiddleware
    StatisticsMiddleware


Include commons:
    BaseController
    BaseControllerWithIdentity
    TokenBucketLimitingService
    LeakageBucketLimitingService
    Platform




How to use :

1.import these packages：
  <PackageReference Include="Microsoft.AspNetCore.Http" Version="2.0.3" />
    <PackageReference Include="Microsoft.AspNetCore.Identity" Version="2.0.3" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="2.0.4" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.0.3" />
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="2.0.2" />
    <PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="2.0.2" />
    <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="2.0.2" />
    <PackageReference Include="Microsoft.Extensions.Identity.Stores" Version="2.0.3" />
    <PackageReference Include="Microsoft.Extensions.Options.ConfigurationExtensions" Version="2.0.2" />


2.using Pure.NetCoreExtensions

3. Config in aspnet core Startup.cs
   public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=HelloWorld}/{action=Index}/{id?}");
            });
            


            //global middleware
            app.UseGlobalHostingEnvironment(env)
                .UseGlobalHttpContext()
                .UseGlobalLoggerFactory()
                .UseGlobalErrorHandling()
               ; 

         
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
		
		
		
4.go !

![image](https://raw.githubusercontent.com/purestackorg/Pure.NETCoreExtensions/master/Pure.NetCoreExtensions.Test/1.jpg)



reference:		
		
		
		
		