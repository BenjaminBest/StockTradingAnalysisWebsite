using System;
using System.ComponentModel.DataAnnotations;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Web.Models
{
    public class CalculationViewModel : ICalculation
    {
        public Guid Id { get; set; }

        public int OriginalVersion { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationNameRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationName", ResourceType = typeof (Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationWKNRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationWKN", ResourceType = typeof (Resources))]
        public string Wkn { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationMultiplierRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationMultiplier", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F4}", ApplyFormatInEditMode = true, NullDisplayText = "0,0000")]
        public decimal Multiplier { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationStrikePriceRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationStrikePrice", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal? StrikePrice { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationUnderlyingRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationUnderlying", ResourceType = typeof (Resources))]
        public string Underlying { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationInitialSLRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationInitialSL", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00 €")]
        public decimal InitialSl { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationInitialTPRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationInitialTP", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00 €")]
        public decimal InitialTp { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationPricePerUnitRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationPricePerUnit", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal PricePerUnit { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationOrderCostsRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationOrderCosts", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal OrderCosts { get; set; }

        [Display(Name = "Display_CalculationDescription", ResourceType = typeof (Resources))]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "-")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationUnitsRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationUnits", ResourceType = typeof (Resources))]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true, NullDisplayText = "0,00")]
        public decimal Units { get; set; }

        [Required(ErrorMessageResourceName = "Validation_CalculationIsLongRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_CalculationIsLong", ResourceType = typeof (Resources))]
        public bool IsLong { get; set; }
    }
}