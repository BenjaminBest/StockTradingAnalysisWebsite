using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Domain
{
    public interface ICalculationResult
    {
        /// <summary>
        /// WKN
        /// </summary>
        string Wkn { get; set; }

        /// <summary>
        /// Average true range
        /// </summary>
        decimal Atr { get; set; }

        /// <summary>
        /// Chance risk ratio
        /// </summary>
        decimal Crv { get; set; }

        /// <summary>
        /// Chance risk ratio
        /// </summary>
        decimal PositionSize { get; set; }

        /// <summary>
        /// Range of point between buying quotation and break even
        /// </summary>
        /// <remarks>
        /// if <seealso cref="IsUnderlyingUsed"/> is <c>true</c> then the underlying is used
        /// </remarks>
        decimal PointsToBe { get; }

        /// <summary>
        /// Range of point between break even and buying quotation
        /// </summary>
        /// <remarks>
        /// if <seealso cref="IsUnderlyingUsed"/> is <c>true</c> then the underlying is used
        /// </remarks>
        decimal PointsToSl { get; }

        /// <summary>
        /// Range of point between buying quotation and take profit
        /// </summary>
        /// <remarks>
        /// if <seealso cref="IsUnderlyingUsed"/> is <c>true</c> then the underlying is used
        /// </remarks>
        decimal PointsToTp { get; }

        /// <summary>
        /// List with a few potential profit and loss quotations
        /// </summary>
        List<ICalculationQuotation> Quotation { get; set; }

        /// <summary>
        /// Returns the first part (50%) of <seealso cref="Quotation"/>
        /// </summary>
        List<ICalculationQuotation> FirstPart { get; }

        /// <summary>
        /// Returns the last part (50%) of <seealso cref="Quotation"/>
        /// </summary>
        List<ICalculationQuotation> LastPart { get; }

        /// <summary>
        /// Returns <c>true</c> if a underlying quotation is used
        /// </summary>
        bool IsUnderlyingUsed { get; set; }

        /// <summary>
        /// Returns the quotation information for break even
        /// </summary>
        ICalculationQuotation BreakEven { get; }

        /// <summary>
        /// Returns the quotation information for price per unit
        /// </summary>
        ICalculationQuotation Buy { get; }

        /// <summary>
        /// Returns the quotation information for stopp loss
        /// </summary>
        ICalculationQuotation StoppLoss { get; }

        /// <summary>
        /// Returns the quotation information for take profit
        /// </summary>
        ICalculationQuotation TakeProfit { get; }

        /// <summary>
        /// Returns the quotation information for the current market price
        /// </summary>
        ICalculationQuotation CurrentQuotation { get; }
    }
}