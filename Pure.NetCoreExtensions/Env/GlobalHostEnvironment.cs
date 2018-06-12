using Microsoft.AspNetCore.Hosting;
namespace Pure.NetCoreExtensions
{
    public class GlobalHostEnvironment
    { 
        private static IHostingEnvironment _env;

        public static  IHostingEnvironment Current => _env;
        internal static void Configure(IHostingEnvironment env)
        {
            _env = env;
        }

        public static string WebRootPath { get { return Current.WebRootPath; } }
        public static string ContentRootPath { get { return Current.ContentRootPath; } }
        public static string EnvironmentName { get { return Current.EnvironmentName; } }
        public static string ApplicationName { get { return Current.ApplicationName; } }



    }

}