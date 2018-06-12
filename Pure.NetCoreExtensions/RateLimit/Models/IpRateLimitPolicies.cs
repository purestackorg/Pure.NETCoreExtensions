using System.Collections.Generic;

namespace Pure.NetCoreExtensions.RateLimit
{
    public class IpRateLimitPolicies
    {
        public List<IpRateLimitPolicy> IpRules { get; set; }
    }
}
