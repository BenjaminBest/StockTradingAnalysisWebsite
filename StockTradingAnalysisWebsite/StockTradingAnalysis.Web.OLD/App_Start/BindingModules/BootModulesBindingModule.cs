using Ninject.Modules;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for boot modules
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class BootModulesBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Kernel.FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(IBootModule));
        }
    }
}