using Microsoft.Extensions.Logging;

namespace Pure.NetCoreExtensions
{
    public class GlobalLoggerFactory
    { 
        private static ILoggerFactory svr;

        public static ILoggerFactory Current => svr;
        internal static void Configure(ILoggerFactory _svr)
        {
            svr = _svr;
        }

      



    }

}