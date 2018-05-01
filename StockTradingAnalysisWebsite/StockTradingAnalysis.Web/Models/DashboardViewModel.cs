namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// The DashboardViewModel defines the model for the dashboard.
    /// </summary>
    public class DashboardViewModel
    {
        /// <summary>
        /// Gets or sets the cards.
        /// </summary>
        /// <value>
        /// The cards.
        /// </value>
        public CardsViewModel Cards { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        public DashboardViewModel()
        {
            Cards = new CardsViewModel();
        }
    }
}