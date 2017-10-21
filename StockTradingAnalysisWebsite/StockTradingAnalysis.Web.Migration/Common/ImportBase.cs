using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Core.Services;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Interfaces.Queries;

namespace StockTradingAnalysis.Web.Migration.Common
{
    public class ImportBase
    {
        protected readonly ILoggingService LoggingService;
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IQueryDispatcher QueryDispatcher;
        protected readonly IConfigurationRegistry Configuration;
        protected readonly JsonSerializerService SerializerService;

        public ImportBase()
        {
            LoggingService = DependencyResolver.GetService<ILoggingService>();
            CommandDispatcher = DependencyResolver.GetService<ICommandDispatcher>();
            QueryDispatcher = DependencyResolver.GetService<IQueryDispatcher>();
            Configuration = DependencyResolver.GetService<IConfigurationRegistry>();
            SerializerService = new JsonSerializerService();
        }
    }
}
