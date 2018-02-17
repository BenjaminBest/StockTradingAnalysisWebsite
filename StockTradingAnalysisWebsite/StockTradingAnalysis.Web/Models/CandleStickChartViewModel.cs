using System;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The view model CandleStickChartViewModel is used for financial candlestick charts
    /// </summary>
    public class CandleStickChartViewModel
    {
        /// <summary>
        /// Gets or sets the data URL.
        /// </summary>
        /// <value>
        /// The data URL.
        /// </value>
        public Guid StockId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CandleStickChartViewModel" /> class.
        /// </summary>
        /// <param name="stockId">The stock identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="height">The height.</param>
        public CandleStickChartViewModel(Guid stockId, string title, int height)
        {
            StockId = stockId;
            Title = title;
            Height = height;
        }
    }
}