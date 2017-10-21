using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class FeedbackViewModel
    {
        public Guid Id { get; set; }

        public int OriginalVersion { get; set; }

        [Required(ErrorMessageResourceName = "Validation_FeedbackNameRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_FeedbackName", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Validation_FeedbackDescriptionRequired",
            ErrorMessageResourceType = typeof(Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_FeedbackDescription", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}