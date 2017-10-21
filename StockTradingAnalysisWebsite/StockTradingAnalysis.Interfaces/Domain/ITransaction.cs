using System;
using StockTradingAnalysis.Interfaces.ReadModel;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for a transaction, this can be either a buying or a selling transaction
    /// </summary>
    public interface ITransaction : IVersionableModelRepositoryItem
    {
        /// <summary>
        /// Gets the order date
        /// </summary>
        DateTime OrderDate { get; }

        /// <summary>
        /// Gets the amount of units
        /// </summary>
        decimal Shares { get; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        decimal PricePerShare { get; }

        /// <summary>
        /// Gets the order costs
        /// </summary>
        decimal OrderCosts { get; }

        /// <summary>
        /// Gets the description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the tag
        /// </summary>
        string Tag { get; }

        /// <summary>
        /// Gets the image
        /// </summary>
        IImage Image { get; }

        /// <summary>
        /// Gets the underlying stock
        /// </summary>
        IStock Stock { get; }

        /// <summary>
        /// Gets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        decimal PositionSize { get; }

        /// <summary>
        /// Gets a transaction description if this was a buy, sell or dividend
        /// </summary>        
        string Action { get; }
    }
}