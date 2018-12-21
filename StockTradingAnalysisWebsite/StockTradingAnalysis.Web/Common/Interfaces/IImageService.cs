using System;
using System.Web;
using Microsoft.AspNetCore.Http;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
	/// <summary>
	/// The interface IImageService defines an service which can analyse images
	/// </summary>
	public interface IImageService
	{
		/// <summary>
		/// Returns the image as byte array and meta information
		/// </summary>
		/// <param name="file">The uploaded image</param>
		/// <param name="description">The description</param>
		/// <param name="id">The id of the image</param>
		/// <returns><c>True</c>if an image was found</returns>
		IImage GetImage(IFormFile file, string description, Guid id);

		/// <summary>
		/// Returns the image as byte array and meta information
		/// </summary>
		/// <param name="contentType">The content type</param>
		/// <param name="description">The description</param>
		/// <param name="id">The id of the image</param>
		/// <param name="data">The byte array</param>
		/// <param name="originalName">The original name</param>
		/// <returns><c>True</c>if an image was found</returns>
		IImage GetImage(byte[] data, string originalName, string contentType, string description, Guid id);
	}
}