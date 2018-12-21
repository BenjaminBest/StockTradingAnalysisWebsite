using Ninject.Modules;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Web.Common.Formatting;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for frontend formatting
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class FormattingBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Repository
            Bind<IStatisticCardConverterRepository>().To<StatisticCardConverterRepository>().InSingletonScope();

            //Converters
            Kernel.FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(IStatisticCardConverter));
        }
    }
}