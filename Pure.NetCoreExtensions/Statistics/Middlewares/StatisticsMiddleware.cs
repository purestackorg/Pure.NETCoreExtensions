using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions
{
    public class StatisticsMiddleware
    {
        private readonly RequestDelegate next;

        public StatisticsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(GlobalHttpContext context)
        {
            Task.Factory.StartNew(async ()=> {

                // Get services
                var geo = context.RequestServices.GetRequiredService<IGeolocationProvider>();

                // Get significant arguments
                var url = context.Request.Scheme + "://" + context.Request.Host + "/" + context.Request.Path;
                var ip = context.Connection.RemoteIpAddress.ToString();
                var geolocation = await geo.GeolocateAsync(ip);
            });
            return Task.FromResult(0);
        }
    }
}
