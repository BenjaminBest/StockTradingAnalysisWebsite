using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class StockViewModel
    {
        public Guid Id { get; set; }

        public int OriginalVersion { get; set; }

        public string StocksDescription { get; set; }

        public string StocksShortDescription { get; set; }

        [Required(ErrorMessageResourceName = "Validation_StockNameRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StockName", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Validation_StockWKNRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StockWKN", ResourceType = typeof(Resources))]
        public string Wkn { get; set; }

        [Required(ErrorMessageResourceName = "Validation_StockTypeIDRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StockType", ResourceType = typeof(Resources))]
        public string Type { get; set; }

        [Required(ErrorMessageResourceName = "Validation_StockLongShortRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_LongShort", ResourceType = typeof(Resources))]
        [UIHint("LongShort")]
        public string LongShort { get; set; }

        [UIHint("PerformanceAbsolute")]
        public decimal Performance { get; set; }
    }
}