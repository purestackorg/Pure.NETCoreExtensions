using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pure.NetCoreExtensions.Test
{
    public class Program
    {
        static private IConfigurationRoot config = null;
        public static void Main(string[] args)
        {
            config = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("hosting.json", optional: true)
         .Build();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args) 
            .UseKestrel()
            .UseConfiguration(config)
                .UseStartup<Startup>()
              .UseContentRoot(Directory.GetCurrentDirectory())
    .UseIISIntegration()
              //.UseHttpSys()
              .UseApplicationInsights()
                .Build();


    }
}
