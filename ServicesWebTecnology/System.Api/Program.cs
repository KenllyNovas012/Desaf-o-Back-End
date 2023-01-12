using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.WebApi.Extensions;

namespace System.Api
{
    public class Program
    {
        private static readonly string Namespace = typeof(Startup).Namespace;

        private static readonly string AppName = Namespace?[(Namespace.LastIndexOf('.',
            Namespace.LastIndexOf('.') - 1) + 1)..] ?? "";

        private static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
           
            try
            {
                var host = CreateHostBuilder(args).Build().InitDatabase();
                host.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Fatal(e, "Application failed to start");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
