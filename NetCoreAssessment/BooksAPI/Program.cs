using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace BooksAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var nLogger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                nLogger.Error(string.Format("Error in initialization: {0}", ex));
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).
            //UseKestrel().
            UseIISIntegration().
            ConfigureLogging((hostingcontext, logging) =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            }).
            UseStartup<Startup>().
            UseNLog();
    }
}
