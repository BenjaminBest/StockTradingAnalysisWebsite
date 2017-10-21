namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a buying (opening) transaction
    /// </summary>
    public interface IBuyingTransaction : ITransaction
    {
        /// <summary>
        /// Gets the initial stop loss
        /// </summary>
        decimal InitialSL { get; }

        /// <summary>
        /// Gets the initial take profit
        /// </summary>
        decimal InitialTP { get; }

        /// <summary>
        /// Gets the strategy used
        /// </summary>
        IStrategy Strategy { get; }

        /// <summary>
        /// Gets/sets the change risk ratio = TP Points/SL Points
        /// </summary>
        decimal CRV { get; }
    }
}