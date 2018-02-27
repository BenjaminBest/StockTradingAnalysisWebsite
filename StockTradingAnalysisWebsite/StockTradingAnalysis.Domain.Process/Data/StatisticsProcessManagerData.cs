using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Process.Data
{
    /// <summary>
    /// The StatisticsProcessManagerData contains all data relates to the statistics process.
    /// </summary>
    public class StatisticsProcessManagerData : IProcessManagerData
    {
        /// <summary>
        /// Gets or sets a value indicating whether the replay was finished.
        /// </summary>
        /// <value>
        ///   <c>true</c> if replay was finished; otherwise, <c>false</c>.
        /// </value>
        public bool ReplayFinished { get; set; }
    }
}
