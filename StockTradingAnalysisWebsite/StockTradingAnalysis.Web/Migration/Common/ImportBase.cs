using Microsoft.Extensions.Configuration;
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
        protected readonly string SourceConnectionString;
        protected readonly string TargetConnectionString;

        public ImportBase()
        {
            LoggingService = DependencyResolver.Current.GetService<ILoggingService>();
            CommandDispatcher = DependencyResolver.Current.GetService<ICommandDispatcher>();
            QueryDispatcher = DependencyResolver.Current.GetService<IQueryDispatcher>();
            Configuration = DependencyResolver.Current.GetService<IConfigurationRegistry>();
            SerializerService = new JsonSerializerService();
            SourceConnectionString = DependencyResolver.Current.GetService<IConfiguration>().GetConnectionString("StockTradingAnalysis_MSSQL_Migration_Source");
            TargetConnectionString = DependencyResolver.Current.GetService<IConfiguration>().GetConnectionString("StockTradingAnalysis_MSSQL_Migration_Target");
        }
    }
}
