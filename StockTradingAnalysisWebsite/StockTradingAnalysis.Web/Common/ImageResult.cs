using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StockTradingAnalysis.Web.Common
{
	/// <summary>
	/// Class ImageResult contains an image
	/// </summary>
	public class ImageResult : ActionResult
	{
		/// <summary>
		/// Contenttype
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Byte-Array
		/// </summary>
		public byte[] ImageBytes { get; set; }

		/// <summary>
		/// Filename
		/// </summary>
		public string SourceFilename { get; set; }

		/// <summary>
		/// Initializes this object with the given arguments
		/// </summary>
		/// <remarks>
		/// This is used for times where you have a physical location
		/// </remarks>
		/// <param name="sourceFilename">File location</param>
		/// <param name="contentType">Contenttype</param>
		public ImageResult(string sourceFilename, string contentType)
		{
			SourceFilename = sourceFilename;
			ContentType = contentType;
		}

		/// <summary>
		/// Initializes this object with the given arguments
		/// </summary>
		/// <remarks>
		/// This is used for when you have the actual image in byte form
		///  which is more important for this post.
		/// </remarks>
		/// <param name="sourceStream">Bytearray</param>
		/// <param name="contentType">Content type</param>
		public ImageResult(byte[] sourceStream, string contentType)
		{
			ImageBytes = sourceStream;
			ContentType = contentType;
		}

		/// <summary>
		/// Loads an image from bytearray or from file location
		/// </summary>
		/// <param name="context">Context</param>
		public override void ExecuteResult(ActionContext context)
		{
			var response = context.HttpContext.Response;
			response.Clear();
			response.ContentType = ContentType;

			if (ImageBytes != null)
			{
				var stream = new MemoryStream(ImageBytes);
				stream.WriteTo(response.Body);
				stream.Dispose();
			}
		}
	}
}