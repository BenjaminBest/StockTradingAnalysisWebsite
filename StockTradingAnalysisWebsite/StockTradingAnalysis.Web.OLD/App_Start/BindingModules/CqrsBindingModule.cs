using Ninject.Modules;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.CQRS.CommandDispatcher;
using StockTradingAnalysis.CQRS.QueryDispatcher;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Web.BindingModules
{
    /// <summary>
    /// Binding module for CQRS
    /// </summary>
    /// <seealso cref="NinjectModule" />
    public class CqrsBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //CQRS Queries
            Bind<IQueryDispatcher>().To<QueryDispatcher>().InSingletonScope();

            Kernel.FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(IQueryHandler<,>));


            //CQRS Commands
            Bind<ICommandDispatcher>().To<CommandDispatcher>().InSingletonScope();

            Kernel.FindAllInterfacesOfType("StockTradingAnalysis.*.dll", typeof(ICommandHandler<>));
        }
    }
}