using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using Serilog;

namespace UserManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", true)
                .Build();

            var url = config.GetValue<string>("Url");

            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseKestrel().UseUrls(url).UseStartup<Startup>())
                .UseSerilog((hostBuilderContext, logConfig) => logConfig.ReadFrom.Configuration(hostBuilderContext.Configuration).Enrich.FromLogContext())
                .UseWindowsService();
        }
    }
}
