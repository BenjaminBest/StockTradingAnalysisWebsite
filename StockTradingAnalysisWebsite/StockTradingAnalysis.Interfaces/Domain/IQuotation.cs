using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// The interface IQuotation defines a single quotation for a share
    /// </summary>
    public interface IQuotation
    {
        /// <summary>
        /// Date
        /// </summary>
        DateTime Date { get; }

        /// <summary>
        /// Changed
        /// </summary>
        DateTime Changed { get; }

        /// <summary>
        /// Open
        /// </summary>
        decimal Open { get; }

        /// <summary>
        /// Close
        /// </summary>
        decimal Close { get; }

        /// <summary>
        /// High
        /// </summary>
        decimal High { get; }

        /// <summary>
        /// Low
        /// </summary>
        decimal Low { get; }
    }
}