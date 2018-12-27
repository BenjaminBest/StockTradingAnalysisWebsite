using Hangfire;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Scheduler;
using StockTradingAnalysis.Web.Common.Interfaces;
using System;
using System.Linq;

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
		/// <param name="queryDispatcher">The query dispatcher.</param>
		/// <param name="commandDispatcher">The command dispatcher.</param>
		/// <param name="quotationServiceClient">The quotation service client.</param>
		public UpdateQuotationsScheduledJob(
			IQueryDispatcher queryDispatcher,
			ICommandDispatcher commandDispatcher,
			IQuotationServiceClient quotationServiceClient)
		{
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
			_quotationServiceClient = quotationServiceClient;
		}

		/// <summary>
		/// Executes this job.
		/// </summary>
		public void Execute()
		{
			//TODO: Duplicate code, see QuotationController.UpdateQuotation

			foreach (var stock in _queryDispatcher.Execute(new StockAllQuery()))
			{
				var latestUpdate = stock.Quotations != null && stock.Quotations.Any() ? stock.Quotations.Max(q => q.Changed) : DateTime.MinValue;

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