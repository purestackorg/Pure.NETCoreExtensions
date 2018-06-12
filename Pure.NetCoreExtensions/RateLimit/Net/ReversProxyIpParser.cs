using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Pure.NetCoreExtensions.RateLimit
{
    public class ReversProxyIpParser : RemoteIpParser
    {
        private readonly string _realIpHeader;

        public ReversProxyIpParser(string realIpHeader)
        {
            _realIpHeader = realIpHeader;
        }

        public override IPAddress GetClientIp(Microsoft.AspNetCore.Http.HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(_realIpHeader, StringComparer.CurrentCultureIgnoreCase))
            {
                return ParseIp(context.Request.Headers[_realIpHeader].Last());
            }

            return base.GetClientIp(context);
        }
    }
}
