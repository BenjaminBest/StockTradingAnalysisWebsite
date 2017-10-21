namespace StockTradingAnalysis.Interfaces.Domain
{
    public interface ICalculationQuotation
    {
        /// <summary>
        /// Gets/sets the name
        /// </summary>
        decimal Quotation { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        decimal QuotationUnderlying { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        decimal PlAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        decimal PlPercentage { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at break even
        /// </summary>
        bool IsBreakEven { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the stopp loss
        /// </summary>
        bool IsStoppLoss { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the take profit
        /// </summary>
        bool IsTakeProfit { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if this is the current quotation
        /// </summary>
        bool IsCurrentQuotation { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the buying quotation
        /// </summary>
        bool IsBuy { get; set; }
    }
}