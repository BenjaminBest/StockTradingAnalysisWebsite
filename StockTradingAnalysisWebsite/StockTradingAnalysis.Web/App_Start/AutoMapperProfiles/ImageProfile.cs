using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
	/// <summary>
	/// The ImageProfile contains the auto mapper configuration for an image, <see cref="IImage"/>.
	/// </summary>
	/// <seealso cref="Profile" />
	public class ImageProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageProfile"/> class.
		/// </summary>
		public ImageProfile()
		{
			CreateMap<IImage, ImageViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
				.ForMember(t => t.ContentType, source => source.MapFrom(s => s.ContentType))
				.ForMember(t => t.OriginalName, source => source.MapFrom(s => s.OriginalName))
				.ForMember(t => t.Data, source => source.MapFrom(s => s.Data));
		}
	}
}