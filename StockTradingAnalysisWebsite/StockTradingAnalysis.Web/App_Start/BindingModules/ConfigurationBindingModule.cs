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
            //Configuration Registry
            Kernel.Bind<IConfigurationRegistry>().ToMethod((context) =>
            {
                var registry = new ConfigurationRegistry();

                registry.AddValue("StockQuoteServiceBaseUrl", ConfigurationManager.AppSettings["StockQuoteServiceBaseUrl"]);
                registry.AddValue("StockQuoteOnlineCheckUrl", ConfigurationManager.AppSettings["StockQuoteOnlineCheckUrl"]);
                registry.AddValue("StockTradingAnalysis_MSSQL", ConfigurationManager.ConnectionStrings["StockTradingAnalysis_MSSQL"].ConnectionString);
                registry.AddValue("StockTradingAnalysis_RavenDB", ConfigurationManager.ConnectionStrings["StockTradingAnalysis_RavenDB"].ConnectionString);

                return registry;

            }).InSingletonScope();
        }
    }
}