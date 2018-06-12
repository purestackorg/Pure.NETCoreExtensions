using System.Collections.Generic;

namespace Pure.NetCoreExtensions.RateLimit
{
    public class ClientRateLimitPolicies
    {
        public List<ClientRateLimitPolicy> ClientRules { get; set; }
    }
}
