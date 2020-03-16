namespace StockTradingAnalysis.Interfaces.Scheduler
{
    /// <summary>
    /// Defines the status of a scheduled job
    /// </summary>
    public enum ScheduledJobStatus
    {
        /// <summary>
        /// Currently not running
        /// </summary>
        Stopped,
        /// <summary>
        /// Currently running
        /// </summary>
        Running
    }
}