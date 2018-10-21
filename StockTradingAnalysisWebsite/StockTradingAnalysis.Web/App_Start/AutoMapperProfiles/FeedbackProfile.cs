using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
	/// <summary>
	/// The FeedbackProfile contains the mapper configuration for <see cref="IFeedback"/> and <see cref="IFeedbackProportion"/>.
	/// </summary>
	/// <seealso cref="Profile" />
	public class FeedbackProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FeedbackProfile"/> class.
		/// </summary>
		public FeedbackProfile()
		{
			CreateMap<IFeedback, FeedbackViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
				.ForMember(t => t.Name, source => source.MapFrom(s => s.Name))
				.ForMember(t => t.Description, source => source.MapFrom(s => s.Description));

			CreateMap<IFeedback, SelectionViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.Text, source => source.MapFrom(s => s.Name));

			CreateMap<IFeedbackProportion, FeedbackProportionViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.Proportion, source => source.MapFrom(s => s.ProportionPercentage));
		}
	}
}