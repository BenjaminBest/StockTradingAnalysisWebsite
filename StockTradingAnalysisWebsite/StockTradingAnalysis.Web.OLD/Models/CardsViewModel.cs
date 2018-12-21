using System.Collections.Generic;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The CardsViewModel defines the data to display a list of cards.
    /// </summary>
    public class CardsViewModel
    {
        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        /// <value>
        /// The cards.
        /// </value>
        public IList<CardViewModel> Cards { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsViewModel"/> class.
        /// </summary>
        public CardsViewModel()
        {
            Cards = new List<CardViewModel>();
        }
    }
}