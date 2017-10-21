using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.BootModules
{
    /// <summary>
    /// Defines an boot module for configuration purposes
    /// </summary>
    public class ConfigurationBootModule : IBootModule
    {
        /// <summary>
        /// Gets the priority
        /// </summary>
        public int Priority => -1;

        /// <summary>
        /// Boots up the module
        /// </summary>
        public void Boot()
        {
            //Create all projections from the event history
            DependencyResolver.GetService<IEventStoreInitializer>().Replay();
        }
    }
}