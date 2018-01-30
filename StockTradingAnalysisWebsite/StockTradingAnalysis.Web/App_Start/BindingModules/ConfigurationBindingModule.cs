using System.Configuration;
using Ninject.Modules;
using StockTradingAnalysis.Core.Configuration;
using StockTradingAnalysis.Interfaces.Configuration;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class ConfigurationBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            var configurationManager = ConfigurationManager.AppSettings;

            //Configuration Registry
            Kernel.Bind<IConfigurationRegistry>().ToMethod((context) =>
            {
                var registry = new ConfigurationRegistry();

                registry.AddValue("StockQuoteServiceBaseUrl", configurationManager["StockQuoteServiceBaseUrl"]);
                registry.AddValue("StockQuoteOnlineCheckUrl", configurationManager["StockQuoteOnlineCheckUrl"]);

                return registry;

            }).InSingletonScope();
        }
    }
}