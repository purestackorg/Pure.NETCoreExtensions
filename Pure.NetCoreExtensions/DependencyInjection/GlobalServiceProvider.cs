using System;

namespace Pure.NetCoreExtensions
{
    public class GlobalServiceProvider
    { 
        private static IServiceProvider _service;

        public static IServiceProvider Current => _service;
        internal static void Configure(IServiceProvider svr)
        {
            _service = svr;
        }

       


    }

}