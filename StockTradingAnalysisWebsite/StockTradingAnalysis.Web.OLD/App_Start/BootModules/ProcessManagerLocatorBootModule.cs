using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.BootModules
{
    /// <summary>
    /// Defines an boot module for the process manager locator.
    /// </summary>
    public class ProcessManagerLocatorBootModule : IBootModule
    {
        /// <summary>
        /// Gets the priority
        /// </summary>
        public int Priority => 20;

        /// <summary>
        /// Boots up the module
        /// </summary>
        public void Boot()
        {
            //Create all projections from the event history
            DependencyResolver.GetService<IProcessManagerCoordinator>().Initialize();
        }
    }
}