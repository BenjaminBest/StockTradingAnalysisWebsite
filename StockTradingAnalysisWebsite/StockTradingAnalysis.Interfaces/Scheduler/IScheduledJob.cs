using System.Dynamic;

namespace StockTradingAnalysis.Interfaces.Scheduler
{
	/// <summary>
	/// The interface IScheduledJob defines a job which will be scheduled for recurring execution.
	/// </summary>
	public interface IScheduledJob
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get;}

		/// <summary>
		/// Gets the interval as cron syntax.
		/// </summary>
		/// <value>
		/// The cron interval.
		/// </value>
		string CronExpression { get; }

        /// <summary>
        /// Status of this job
        /// </summary>
        ScheduledJobStatus Status { get; }

		/// <summary>
		/// Executes this job.
		/// </summary>
		void Execute();
    }
}