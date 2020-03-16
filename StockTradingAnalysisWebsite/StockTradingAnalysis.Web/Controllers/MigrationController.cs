using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Entities;
using StockTradingAnalysis.Web.Migration.Importer;

namespace StockTradingAnalysis.Web.Controllers
{
    /// <summary>
    /// The MigrationController deletes all data in the current database and imports the legacy data into the new system.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class MigrationController : Controller
    {
        /// <summary>
        /// The model repository deletion coordinator
        /// </summary>
        private readonly IModelRepositoryDeletionCoordinator _modelRepositoryDeletionCoordinator;

        /// <summary>
        /// The logging service
        /// </summary>
        private readonly ILoggingService _loggingService;

        /// <summary>
        /// All scheduled jobs
        /// </summary>
        private readonly IEnumerable<IScheduledJob> _jobs;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationController" /> class.
        /// </summary>
        /// <param name="modelRepositoryDeletionCoordinator">The model repository deletion coordinator.</param>
        /// <param name="loggingService">The logging service.</param>
        public MigrationController(
            IModelRepositoryDeletionCoordinator modelRepositoryDeletionCoordinator,
            ILoggingService loggingService,
            IEnumerable<IScheduledJob> jobs)
        {
            _modelRepositoryDeletionCoordinator = modelRepositoryDeletionCoordinator;
            _loggingService = loggingService;
            _jobs = jobs;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Migrate(int id)
        {
            var result = string.Empty;

            var stock = new ImportStock();
            var feedback = new ImportFeedback();
            var strategy = new ImportStrategy();
            var transaction = new ImportTransaction();

            var quotationJob = _jobs.FirstOrDefault(j => j.Name == "Update Quotations");

            //TODO: Application is not able to handle multithreading properly, we need to make sure nothing else is importing
            for (var numberOfTry = 0; numberOfTry < 10; numberOfTry++)
            {
                try
                {
                    //TODO: Check all jobs
                    if (quotationJob?.Status != ScheduledJobStatus.Running)
                    {
                        break;
                    }
                }
                catch
                {
                    // ignored
                }

                await Task.Delay(10000);
            }


            switch (id)
            {
                case 0: //Erase Database
                {
                    PerformanceStats.Reset();
                    _modelRepositoryDeletionCoordinator.DeleteAll();
                    result = "Deleted data from current application database";
                    break;
                }
                case 1: //Import stocks
                {
                    stock.Start();
                    result = "Imported stocks";
                    new MigrationItemPersister<StockDto>("stock").Set(stock.Items);
                    new MigrationItemPersister<StockDto>("dividendstock").Set(stock.DividendItems);
                    break;
                }
                case 2: //Import quotations
                {
                    var quotations = new ImportQuotations();
                    quotations.StockItems = new MigrationItemPersister<StockDto>("stock").Get();
                    quotations.Start();

                    result = "Imported quotations";
                    break;
                }
                case 3: //Download quotations
                {
                    var downloadQuotations = new DownloadQuotations();
                    downloadQuotations.Start();
                    result = "Downloaded quotations";
                    break;
                }
                case 4: //Import feedbacks
                {
                    feedback.Start();
                    result = "Imported feedbacks";
                    new MigrationItemPersister<FeedbackDto>("feedback").Set(feedback.Items);
                    break;
                }
                case 5: //Import strategies
                {
                    strategy.Start();
                    result = "Imported strategies";
                    new MigrationItemPersister<StrategyDto>("strategy").Set(strategy.Items);
                    break;
                }
                case 6: //Import calculations
                {
                    var calculation = new ImportCalculations();
                    calculation.Start();
                    result = "Imported calculations";
                    break;
                }
                case 7: //Import transactions
                {
                    transaction.FeedbackItems = new MigrationItemPersister<FeedbackDto>("feedback").Get();
                    transaction.StockItems = new MigrationItemPersister<StockDto>("stock").Get();
                    transaction.StrategyItems = new MigrationItemPersister<StrategyDto>("strategy").Get();
                    transaction.DividendStockItems = new MigrationItemPersister<StockDto>("dividendstock").Get();

                    transaction.Start();
                    result = "Imported transactions";
                    new MigrationItemPersister<ITransactionDto>("transaction").Set(transaction.Items);
                    break;
                }
                case 8: //Testing queries
                {
                    var testing = new TestQueries();
                    testing.Start();
                    result = "Tested results with queries";
                    break;
                }
                case 9: //Testing performance
                {
                    var stats = new TestPerformance(new MigrationItemPersister<ITransactionDto>("transaction").Get());
                    stats.Start();
                    result = "Finished testing statistics";
                    break;
                }
                case 10: //Testing open positions
                {
                    var openPositions = new TestOpenPositions(new MigrationItemPersister<StockDto>("stock").Get(),
                        new MigrationItemPersister<ITransactionDto>("transaction").Get());
                    openPositions.Start();
                    result = "Finished testing open positions";
                    break;
                }
                case 11: //Statistics
                {
                    PerformanceStats.WriteToConsole();
                    result = "Flushed performance statistics";
                    break;
                }
                default:
                {

                    break;
                }
            }


            return Json(result);
        }
    }
}