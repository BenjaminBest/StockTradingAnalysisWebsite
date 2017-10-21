using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a feedback
    /// </summary>
    public interface ICalculation : IVersionableModelRepositoryItem
    {
        /// <summary>
        /// Gets/sets the name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets/sets the wkn
        /// </summary>
        string Wkn { get; }

        /// <summary>
        /// Gets/sets the multiplier
        /// </summary>
        decimal Multiplier { get; }

        /// <summary>
        /// Gets/sets the strike price if selling
        /// </summary>
        decimal? StrikePrice { get; }

        /// <summary>
        /// Gets/sets the underlying
        /// </summary>
        string Underlying { get; }

        /// <summary>
        /// Gets/sets the initial stop loss
        /// </summary>
        decimal InitialSl { get; }

        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        decimal InitialTp { get; }

        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        decimal PricePerUnit { get; }

        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        decimal OrderCosts { get; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        decimal Units { get; }

        /// <summary>
        /// Gets/sets if its about selling or buying
        /// </summary>
        bool IsLong { get; }
    }
}