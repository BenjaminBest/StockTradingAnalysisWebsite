using Ninject.Modules;
using StockTradingAnalysis.Services.External.Interfaces;
using StockTradingAnalysis.Services.External.Services;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for the external services
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class ExternalServicesBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Services
            Bind<IStockQuoteExternalService>().To<StockQuoteExternalService>().InSingletonScope();
        }
    }
}