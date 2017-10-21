using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class TransactionToolTipViewModel
    {
        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets/sets the holding-period
        /// </summary>
        [Display(Name = "Display_StatisticsHoldingPeriod", ResourceType = typeof(Resources))]
        public string HoldingPeriod { get; set; }

        /// <summary>
        /// Gets/sets the MAE
        /// </summary>
        [Display(Name = "Display_TooltipMAE", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "-")]
        public decimal? MAE { get; set; }

        /// <summary>
        /// Gets/sets the MFE
        /// </summary>
        [Display(Name = "Display_TooltipMFE", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "-")]
        public decimal? MFE { get; set; }

        /// <summary>
        /// Gets/sets the maximum loss during trade incl. order costs
        /// </summary>
        [Display(Name = "Display_TooltipMAEAbsolute", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "-")]
        public decimal? MAEAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the maximum profit during trade incl. order costs
        /// </summary>
        [Display(Name = "Display_TooltipMFEAbsolute", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "-")]
        public decimal? MFEAbsolute { get; set; }

        /// <summary>
        /// Gets/sets the initial SL
        /// </summary>
        [Display(Name = "Display_TooltipInitialSL", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "-")]
        public decimal InitialSL { get; set; }

        /// <summary>
        /// Gets/sets the initial TP
        /// </summary>
        [Display(Name = "Display_TooltipInitialTP", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = false, NullDisplayText = "-")]
        public decimal InitialTP { get; set; }

        /// <summary>
        /// Gets/sets the exit efficiency 
        /// </summary>
        [UIHint("PerformancePercentageEfficiency")]
        [Display(Name = "Display_StatisticsExitEfficiency", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "-")]
        public decimal? ExitEfficiency { get; set; }

        /// <summary>
        /// Gets/sets the entry efficiency
        /// </summary>
        [UIHint("PerformancePercentageEfficiency")]
        [Display(Name = "Display_StatisticsEntryEfficiency", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "-")]
        public decimal? EntryEfficiency { get; set; }

        /// <summary>
        /// Gets/sets the name of the strategy used
        /// </summary>
        [Display(Name = "Display_StatisticsStrategyName", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "-")]
        public string StrategyName { get; set; }

        /// <summary>
        /// Gets the change risk ratio = TP Points/SL Points
        /// </summary>
        [Display(Name = "ViewTextCRV", ResourceType = typeof(Resources))]
        [DisplayFormat(NullDisplayText = "-")]
        public decimal CRV { get; set; }
    }
}