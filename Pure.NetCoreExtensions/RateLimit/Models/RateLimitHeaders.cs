using Microsoft.AspNetCore.Http;

namespace Pure.NetCoreExtensions.RateLimit
{
    public class RateLimitHeaders
    {
        public Microsoft.AspNetCore.Http.HttpContext Context { get; set; }

        public string Limit { get; set; }

        public string Remaining { get; set; }

        public string Reset { get; set; }
    }
}
