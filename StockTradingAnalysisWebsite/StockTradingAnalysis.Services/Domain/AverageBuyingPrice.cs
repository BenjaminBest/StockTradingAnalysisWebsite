using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Services.Domain
{
    /// <summary>
    /// The AverageBuyingPrice contains an average buying price for a given orderdate.
    /// </summary>
    /// <seealso cref="IAverageBuyingPrice" />
    public class AverageBuyingPrice : IAverageBuyingPrice
    {
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets the average price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AverageBuyingPrice"/> class.
        /// </summary>
        /// <param name="orderDate">The order date.</param>
        /// <param name="averagePrice">The average price.</param>
        public AverageBuyingPrice(DateTime orderDate, decimal averagePrice)
        {
            OrderDate = orderDate;
            AveragePrice = averagePrice;
        }
    }
}
