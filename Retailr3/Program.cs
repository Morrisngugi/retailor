using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Notifyr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

            try
            {
                Log.Warning("Getting the sms system started...");

                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            //RecurringJob.AddOrUpdate("DailySmsTask", () => SmsSebderJob.(), "0 9 * * *");
            //RecurringJob.AddOrUpdate("WeekySmsTask", () => MyMethod(), "0 9 * * *");
            //RecurringJob.AddOrUpdate("MonthlySmsTask", () => MyMethod(), "0 9 * * *");
        }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        // .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .Build();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseApplicationInsights()
            .UseStartup<Startup>()
            .UseConfiguration(Configuration)
            .UseSerilog();
            //.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
            //                          .ReadFrom.Configuration(hostingContext.Configuration));
    }
}
