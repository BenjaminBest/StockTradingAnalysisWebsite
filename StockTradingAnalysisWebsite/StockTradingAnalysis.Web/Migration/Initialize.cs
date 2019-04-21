using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Web.Migration.Common;
using StockTradingAnalysis.Web.Migration.Importer;
using System;

namespace StockTradingAnalysis.Web.Migration
{
	/// <summary>
	/// Initializes and runs the import
	/// </summary>
	public static class Initialize
	{
		/// <summary>
		/// Runs the import.
		/// </summary>
		public static void Run()
		{
			//Initialize & erase database
			Start();
			var logger = Initialize.GetLogger();
			PerformanceStats.Reset();
			logger.Debug("Database erased ...");
			logger.Debug("Initialized binding modules, automapper,infrastructure ...");

			//Import stocks
			logger.Debug("Import stocks ...");
			var stock = new ImportStock();
			stock.Start();
			logger.Debug("... finished importing stocks");

			//Import quotations
			logger.Debug("Import quotations ...");
			var quotations = new ImportQuotations();
			quotations.StockItems = stock.Items;
			quotations.Start();
			logger.Debug("... finished importing quotations");

			//Download quotations
			logger.Debug("Download quotations ...");
			var downloadQuotations = new DownloadQuotations();
			downloadQuotations.Start();
			logger.Debug("... finished importing quotations");

			//Import feedbacks
			logger.Debug("Import feedbacks ...");
			var feedback = new ImportFeedback();
			feedback.Start();
			logger.Debug("... finished importing feedbacks");

			//Import strategies
			logger.Debug("Import strategies ...");
			var strategy = new ImportStrategy();
			strategy.Start();
			logger.Debug("... finished importing strategies");

			//Import calculations
			logger.Debug("Import calculations ...");
			var calculation = new ImportCalculations();
			calculation.Start();
			logger.Debug("... finished importing calculations");

			//Import transactions
			logger.Debug("Import transactions ...");
			var transaction = new ImportTransaction
			{
				FeedbackItems = feedback.Items,
				StockItems = stock.Items,
				StrategyItems = strategy.Items
			};
			transaction.Start();
			logger.Debug("... finished importing transactions");

			//Testing queries
			logger.Debug("Creating test queries ...");
			var testing = new TestQueries();
			testing.Start();
			logger.Debug("... finished testing");

			//Testing performance
			logger.Debug("Testing statistics ...");
			var stats = new TestPerformance(transaction.Items);
			stats.Start();
			logger.Debug("... finished testing statistics");

			//Testing open positions
			logger.Debug("Testing open positions ...");
			var openPositions = new TestOpenPositions(stock.Items, transaction.Items);
			openPositions.Start();
			logger.Debug("... finished testing statistics");

			//Statistics
			logger.Debug("Event Sourcing Performance");
			PerformanceStats.WriteToConsole();


			logger.Debug("Finished...Press any key to close program");
			Console.ReadKey();

			//Stopping everything
			Stop();
		}


		/// <summary>
		/// Initializes the application
		/// </summary>
		public static void Start()
		{
			//Erase Database
			DependencyResolver.Current.GetService<IModelRepositoryDeletionCoordinator>().DeleteAll();
		}

		/// <summary>
		/// Returns the logging service
		/// </summary>
		/// <returns></returns>
		public static ILoggingService GetLogger()
		{
			return DependencyResolver.Current.GetService<ILoggingService>();
		}

		public static void Stop()
		{
		}
	}
}