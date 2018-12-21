using System;
using System.Web.Mvc;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.ModelBinders
{
    /// <summary>
    /// The ImageViewModelBinder converts the image data transfered to a viewmodel.
    /// </summary>
    /// <seealso cref="IModelBinder" />
    public class ImageViewModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// The bound value.
        /// </returns>
        /// <param name="controllerContext">The controller context.</param><param name="bindingContext">The binding context.</param>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;

            var data = request.Form.Get("Image.Data");
            var contentType = request.Form.Get("Image.ContentType");
            var originalName = request.Form.Get("Image.OriginalName");
            var description = request.Form.Get("Image.Description");

            if (string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(originalName) || string.IsNullOrEmpty(data))
                return null;

            return new ImageViewModel()
            {
                ContentType = contentType,
                Data = GetBytes(data),
                Description = description,
                OriginalName = originalName
            };
        }

        /// <summary>
        /// Converts a string to byte array
        /// </summary>
        /// <param name="str">The string</param>
        /// <returns>A byte array</returns>
        private static byte[] GetBytes(string str)
        {
            //TODO: Create converter service and put this there
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        /// <summary>
        /// Converts a byte array to a string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        private static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}