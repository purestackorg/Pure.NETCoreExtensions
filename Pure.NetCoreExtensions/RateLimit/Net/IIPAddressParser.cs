using System.Collections.Generic;
 
using System.Net;

namespace Pure.NetCoreExtensions.RateLimit
{
    public interface IIpAddressParser
    {
        bool ContainsIp(string ipRule, string clientIp);

        bool ContainsIp(List<string> ipRules, string clientIp);

        bool ContainsIp(List<string> ipRules, string clientIp, out string rule);

        IPAddress GetClientIp(Microsoft.AspNetCore.Http.HttpContext context);

        IPAddress ParseIp(string ipAddress);
    }
}
