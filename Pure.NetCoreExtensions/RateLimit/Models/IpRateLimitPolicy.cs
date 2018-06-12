using System.Collections.Generic;

namespace Pure.NetCoreExtensions.RateLimit
{
    public class IpRateLimitPolicy
    {
        public string Ip { get; set; }
        public List<RateLimitRule> Rules { get; set; }
    }
}
