using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DBPub.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        //public static IWebHost BuildWebHost(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //       .UseKestrel()
        //       .UseContentRoot(Directory.GetCurrentDirectory())
        //       .UseIISIntegration()
        //       .UseStartup<Startup>()
        //       .Build();
    }
}
