using System.Linq;
using Hangfire;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.Scheduler
{
	/// <summary>
	/// The UpdateQuotationsScheduledJob will be executed recurrently and updates all quotations.
	/// </summary>
	/// <seealso cref="StockTradingAnalysis.Interfaces.Scheduler.IScheduledJob" />
	public class UpdateQuotationsScheduledJob : IScheduledJob
	{
		/// <summary>
		/// The query dispatcher
		/// </summary>
		private readonly IQueryDispatcher _queryDispatcher;

		/// <summary>
		/// The command dispatcher
		/// </summary>
		private readonly ICommandDispatcher _commandDispatcher;

		/// <summary>
		/// The quotation service client
		/// </summary>
		private readonly IQuotationServiceClient _quotationServiceClient;

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name => "Update Quotations";

		/// <summary>
		/// Gets the interval as cron syntax.
		/// </summary>
		/// <value>
		/// The cron interval.
		/// </value>
		public string CronExpression => Cron.Daily();

		/// <summary>
		/// Initializes a new instance of the <see cref="UpdateQuotationsScheduledJob" /> class.
		/// </summary>		
		public UpdateQuotationsScheduledJob()
		{
			_queryDispatcher = DependencyResolver.GetService<IQueryDispatcher>();
			_commandDispatcher = DependencyResolver.GetService<ICommandDispatcher>(); ;
			_quotationServiceClient = DependencyResolver.GetService<IQuotationServiceClient>(); ;
		}

		/// <summary>
		/// Executes this job.
		/// </summary>
		public void Execute()
		{
			//TODO: Duplicate code, see QuotationController.UpdateQuotation

			foreach (var stock in _queryDispatcher.Execute(new StockAllQuery()))
			{
				var latestUpdate = stock.Quotations.Max(q => q.Changed);

				var quotations = _quotationServiceClient.Get(stock.Id, latestUpdate.Date).ToList();

				if (quotations.Any())
				{
					var cmd = new StockQuotationsAddOrChangeCommand(
						stock.Id,
						stock.OriginalVersion,
						quotations);

					_commandDispatcher.Execute(cmd);
				}
			}
		}
	}
}