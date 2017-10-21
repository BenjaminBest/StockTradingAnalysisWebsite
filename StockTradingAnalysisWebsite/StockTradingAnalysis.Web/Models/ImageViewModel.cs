using System;
using System.ComponentModel.DataAnnotations;

namespace StockTradingAnalysis.Web.Models
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceName = "Validation_ImageContentTypeRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_ImageContentType", ResourceType = typeof (Resources))]
        public string ContentType { get; set; }

        [Required(ErrorMessageResourceName = "Validation_ImageOriginalNameRequired",
            ErrorMessageResourceType = typeof (Resources), AllowEmptyStrings = false)]
        [Display(Name = "Display_ImageOriginalName", ResourceType = typeof (Resources))]
        public string OriginalName { get; set; }

        [Display(Name = "Display_ImageDescription", ResourceType = typeof (Resources))]
        public string Description { get; set; }

        public byte[] Data { get; set; }
    }
}