using log4net.Config;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BootModules
{
    /// <summary>
    /// Defines an boot module for initializing the logging infrastructure
    /// </summary>
    public class LoggingBootModule : IBootModule
    {
        /// <summary>
        /// Gets the priority
        /// </summary>
        public int Priority => 10;

        /// <summary>
        /// Boots up the module
        /// </summary>
        public void Boot()
        {
            //Log4net init
            XmlConfigurator.Configure();
        }
    }
}