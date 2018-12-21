using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
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
			//TODO: Create converter service and put this there
			var chars = new char[bytes.Length / sizeof(char)];
			Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		/// <summary>Attempts to bind a model.</summary>
		/// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
		/// <returns>
		/// <para>
		/// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
		/// </para>
		/// <para>
		/// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
		/// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
		/// </para>
		/// <para>
		/// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
		/// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
		/// </para>
		/// </returns>
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			var request = bindingContext.ActionContext.HttpContext.Request;

			request.Form.TryGetValue("Image.Data", out var data);
			request.Form.TryGetValue("Image.ContentType", out var contentType);
			request.Form.TryGetValue("Image.OriginalName", out var originalName);
			request.Form.TryGetValue("Image.Description", out var description);

			if (string.IsNullOrEmpty(contentType) || string.IsNullOrEmpty(originalName) || string.IsNullOrEmpty(data))
				return Task.CompletedTask;

			bindingContext.Result = ModelBindingResult.Success(new ImageViewModel()
			{
				ContentType = contentType,
				Data = GetBytes(data),
				Description = description,
				OriginalName = originalName
			});

			return Task.CompletedTask;
		}
	}
}