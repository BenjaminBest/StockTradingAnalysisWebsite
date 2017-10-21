using Ninject.Extensions.Conventions;
using Ninject.Modules;
using StockTradingAnalysis.Interfaces.Common;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class BootModulesBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Kernel.Bind(i => i
                .FromAssembliesMatching("StockTradingAnalysis.*.dll")
                .SelectAllClasses()
                .InheritedFrom(typeof (IBootModule))
                .BindSingleInterface()
                );
        }
    }
}