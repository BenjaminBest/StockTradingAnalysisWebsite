using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class StrategyViewModel
    {
        public Guid Id { get; set; }

        public int OriginalVersion { get; set; }

        [Required(ErrorMessageResourceName = "Validation_StrategyNameRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StrategyName", ResourceType = typeof (Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Validation_StrategyDescriptionRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_StrategyDescription", ResourceType = typeof (Resources))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [UIHint("ViewModelImage")]
        public ImageViewModel Image { get; set; }
    }
}