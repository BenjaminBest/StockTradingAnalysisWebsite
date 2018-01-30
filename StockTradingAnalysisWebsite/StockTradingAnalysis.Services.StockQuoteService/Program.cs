using System.IO;
using Microsoft.AspNetCore.Hosting;
using NLog;

namespace StockTradingAnalysis.Services.StockQuoteService
{
    public class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            _logger.Info("Service is running");


            host.Run();

            _logger.Info("Service is stopping");
        }
    }
}
