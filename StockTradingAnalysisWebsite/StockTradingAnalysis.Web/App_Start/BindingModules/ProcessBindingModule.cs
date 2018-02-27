using Ninject.Extensions.Conventions;
using Ninject.Modules;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Domain.Process.Finders;
using StockTradingAnalysis.Domain.Process.Locator;
using StockTradingAnalysis.Domain.Process.ProcessManagers;
using StockTradingAnalysis.Domain.Process.Repository;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for process managers.
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class ProcessBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IProcessManagerRepository>().To<ProcessManagerRepository>().InSingletonScope();
            Bind<IProcessManagerCoordinator>().To<ProcessManagerCoordinator>().InSingletonScope();
            Bind<IProcessManagerFinderRepository>().To<ProcessManagerFinderRepository>().InSingletonScope();

            Bind<IProcessManager>().To<TransactionProcessManager>();
            Bind<IProcessManager>().To<StatisticsProcessManager>();

            Kernel.Bind(i => i
                .FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(IStartedByMessage<>))
                .BindAllInterfaces());

            Kernel.Bind(i => i
                .FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(IMessageCorrelationIdCreator<>))
                .BindAllInterfaces());
        }
    }
}