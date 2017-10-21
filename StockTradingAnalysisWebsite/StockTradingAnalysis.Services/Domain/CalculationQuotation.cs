using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// Class CalculationQuotation defines one single quotation
    /// </summary>
    public class CalculationQuotation : ICalculationQuotation
    {
        /// <summary>
        /// Gets/sets the name
        /// </summary>
        public decimal Quotation { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        public decimal QuotationUnderlying { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        public decimal PlAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the value
        /// </summary>
        public decimal PlPercentage { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at break even
        /// </summary>
        public bool IsBreakEven { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the stopp loss
        /// </summary>
        public bool IsStoppLoss { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the take profit
        /// </summary>
        public bool IsTakeProfit { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if this is the current quotation
        /// </summary>
        public bool IsCurrentQuotation { get; set; }

        /// <summary>
        /// Gets/sets <c>true</c> if <seealso cref="Quotation"/> is at the buying quotation
        /// </summary>
        public bool IsBuy { get; set; }
    }
}