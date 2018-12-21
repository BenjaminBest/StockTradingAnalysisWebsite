using StockTradingAnalysis.Web.Common.Formatting;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The CardViewModel defined the model to display a card box.
    /// </summary>
    public class CardViewModel
    {
        /// <summary>
        /// Gets or sets the header of the card.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the background is filled or the border will indicate the color.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the background] is filled otherwise <c>false</c>.
        /// </value>
        public bool FilledBackground { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        public StyleType Style { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardViewModel"/> class.
        /// </summary>
        public CardViewModel()
        {
            FilledBackground = false;
            Style = StyleType.Primary;
        }
    }
}