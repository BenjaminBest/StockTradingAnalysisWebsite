using Ninject.Extensions.Conventions;
using Ninject.Modules;
using StockTradingAnalysis.CQRS.CommandDispatcher;
using StockTradingAnalysis.CQRS.QueryDispatcher;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class CqrsBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //CQRS Queries
            Kernel.Bind<IQueryDispatcher>().To<QueryDispatcher>().InSingletonScope();

            Kernel.Bind(i => i
                .FromAssembliesMatching("StockTradingAnalysis.*.dll")
                .SelectAllClasses()
                .InheritedFrom(typeof(IQueryHandler<,>))
                .BindSingleInterface()
                );

            //CQRS Commands
            Kernel.Bind<ICommandDispatcher>().To<CommandDispatcher>().InSingletonScope();

            Kernel.Bind(i => i
                .FromAssembliesMatching("StockTradingAnalysis.*.dll")
                .SelectAllClasses()
                .InheritedFrom(typeof(ICommandHandler<>))
                .BindSingleInterface()
                );
        }
    }
}