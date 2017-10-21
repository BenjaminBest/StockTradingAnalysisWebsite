using System;
using System.ComponentModel.DataAnnotations;
using StockTradingAnalysis.Interfaces.Types;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Class TransactionPerformanceViewModel is used for returning the performance (profit, loss..) of a selling/dividend transaction
    /// </summary>
    public class TransactionPerformanceViewModel
    {
        /// <summary>
        /// Gets/sets if profit was made
        /// </summary>
        public bool ProfitMade { get; set; }

        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the profit
        /// </summary>
        [UIHint("PerformanceAbsolute")]
        [Display(Name = "Display_StatisticsProfitAbsolute", ResourceType = typeof(Resources))]
        public decimal ProfitAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the profit (%)
        /// </summary>
        [UIHint("PerformancePercentage")]
        [Display(Name = "Display_StatisticsProfitPercentage", ResourceType = typeof(Resources))]
        public decimal ProfitPercentage { get; set; }

        /// <summary>
        /// Gets/sets the period of time this trade was in the market
        /// </summary>
        [Display(Name = "Display_StatisticsHoldingPeriod", ResourceType = typeof(Resources))]
        public HoldingPeriod HoldingPeriod { get; set; }

        /// <summary>
        /// Gets/sets the risk
        /// </summary>
        [Display(Name = "Display_StatisticsR", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F1}R", ApplyFormatInEditMode = true)]
        public decimal R { get; set; }

        /// <summary>
        /// Gets/sets the  exit efficiency 
        /// </summary>
        [UIHint("PerformancePercentageEfficiency")]
        [Display(Name = "Display_StatisticsExitEfficiency", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "-")]
        public decimal ExitEfficiency { get; set; }

        /// <summary>
        /// Gets/sets the entry efficiency 
        /// </summary>
        [UIHint("PerformancePercentageEfficiency")]
        [Display(Name = "Display_StatisticsEntryEfficiency", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "-")]
        public decimal EntryEfficiency { get; set; }

        /// <summary>
        /// Gets/sets the maximum loss during trade incl. order costs
        /// </summary>
        [Display(Name = "Display_TooltipMAEAbsolute", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        public decimal MAEAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the maximum profit during trade incl. order costs
        /// </summary>
        [Display(Name = "Display_TooltipMFEAbsolute", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "0,00 €")]
        public decimal MFEAbsolute { get; set; }
    }
}