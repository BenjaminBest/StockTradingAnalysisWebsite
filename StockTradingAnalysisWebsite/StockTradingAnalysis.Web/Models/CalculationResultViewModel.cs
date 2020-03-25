using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockTradingAnalysis.Web.Models
{
    /// <summary>
    /// Class ViewModelCalculationResult is used for returning the calculation result
    /// </summary>
    public class CalculationResultViewModel
    {
        /// <summary>
        /// WKN
        /// </summary>
        [Display(Name = "Display_CalculationResultWKN", ResourceType = typeof(Resources))]
        public string Wkn { get; set; }

        /// <summary>
        /// Average true range
        /// </summary>
        [Display(Name = "Display_CalculationResultATR", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "-")]
        public decimal Atr { get; set; }

        /// <summary>
        /// Chance risk ratio
        /// </summary>
        [Display(Name = "Display_CalculationResultCRV", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00")]
        public decimal Crv { get; set; }

        /// <summary>
        /// Chance risk ratio
        /// </summary>
        [Display(Name = "Display_CalculationResultPositionSize", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2} €", ApplyFormatInEditMode = true, NullDisplayText = "0,00 €")]
        [UIHint("Currency")]
        public decimal PositionSize { get; set; }

        /// <summary>
        /// Range of point between buying quotation and break even
        /// </summary>
        /// <remarks>
        /// if <seealso cref="IsUnderlyingUsed"/> is <c>true</c> then the underlying is used
        /// </remarks>
        [Display(Name = "Display_CalculationResultPointsToBE", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00")]
        public decimal PointsToBe
        {
            get
            {
                if (BreakEven == null || Buy == null)
                    return default(decimal);

                decimal result = 0;

                if (IsUnderlyingUsed)
                    result = BreakEven.QuotationUnderlying - Buy.QuotationUnderlying;
                else
                    result = BreakEven.Quotation - Buy.Quotation;

                if (result < 0)
                    result = result * -1;

                return result;
            }
        }

        /// <summary>
        /// Range of point between break even and buying quotation
        /// </summary>
        /// <remarks>
        /// if <seealso cref="IsUnderlyingUsed"/> is <c>true</c> then the underlying is used
        /// </remarks>
        [Display(Name = "Display_CalculationResultPointsToSL", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00")]
        public decimal PointsToSl
        {
            get
            {
                if (Buy == null || StoppLoss == null)
                    return default(decimal);

                decimal result = 0;

                if (IsUnderlyingUsed)
                    result = Buy.QuotationUnderlying - StoppLoss.QuotationUnderlying;
                else
                    result = Buy.Quotation - StoppLoss.Quotation;

                if (result < 0)
                    result = result * -1;

                return result;
            }
        }

        /// <summary>
        /// Range of point between buying quotation and take profit
        /// </summary>
        /// <remarks>
        /// if <seealso cref="IsUnderlyingUsed"/> is <c>true</c> then the underlying is used
        /// </remarks>
        [Display(Name = "Display_CalculationResultPointsToTP", ResourceType = typeof(Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00")]
        public decimal PointsToTp
        {
            get
            {
                if (TakeProfit == null || Buy == null)
                    return default(decimal);

                decimal result = 0;

                if (IsUnderlyingUsed)
                    result = TakeProfit.QuotationUnderlying - Buy.QuotationUnderlying;
                else
                    result = TakeProfit.Quotation - Buy.Quotation;

                if (result < 0)
                    result = result * -1;

                return result;
            }
        }

        /// <summary>
        /// List with a few potential profit and loss quotations
        /// </summary>
        public List<CalculationQuotationViewModel> Quotation { get; set; }

        /// <summary>
        /// Returns the first part (50%) of <seealso cref="Quotation"/>
        /// </summary>
        public List<CalculationQuotationViewModel> FirstPart
        {
            get
            {
                if (Quotation == null || Quotation.Count == 0)
                    return new List<CalculationQuotationViewModel>();

                var half = Quotation.Count / 2;

                return Quotation.GetRange(0, half);
            }
        }

        /// <summary>
        /// Returns the last part (50%) of <seealso cref="Quotation"/>
        /// </summary>
        public List<CalculationQuotationViewModel> LastPart
        {
            get
            {
                if (Quotation == null || Quotation.Count == 0)
                    return new List<CalculationQuotationViewModel>();

                var half = Quotation.Count / 2;

                return Quotation.GetRange(half, Quotation.Count - half);
            }
        }

        /// <summary>
        /// Returns <c>true</c> if a underlying quotation is used
        /// </summary>
        public bool IsUnderlyingUsed { get; set; }

        /// <summary>
        /// Returns the quotation information for break even
        /// </summary>
        public CalculationQuotationViewModel BreakEven
        {
            get { return Quotation?.FirstOrDefault(q => q.IsBreakEven); }
        }

        /// <summary>
        /// Returns the quotation information for price per unit
        /// </summary>
        public CalculationQuotationViewModel Buy
        {
            get { return Quotation?.FirstOrDefault(q => q.IsBuy); }
        }

        /// <summary>
        /// Returns the quotation information for stopp loss
        /// </summary>
        public CalculationQuotationViewModel StoppLoss
        {
            get { return Quotation?.FirstOrDefault(q => q.IsStoppLoss); }
        }

        /// <summary>
        /// Returns the quotation information for take profit
        /// </summary>
        public CalculationQuotationViewModel TakeProfit
        {
            get { return Quotation?.FirstOrDefault(q => q.IsTakeProfit); }
        }

        /// <summary>
        /// Returns the quotation information for the current market price
        /// </summary>
        public CalculationQuotationViewModel CurrentQuotation
        {
            get { return Quotation?.FirstOrDefault(q => q.IsCurrentQuotation); }
        }

        /// <summary>
        /// Initializes this object with default values
        /// </summary>
        public CalculationResultViewModel()
        {
            Quotation = new List<CalculationQuotationViewModel>();
        }
    }
}