using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
	/// <summary>
	/// The StrategyProfile contains the auto mapper configuration for <see cref="IStrategy"/>.
	/// </summary>
	/// <seealso cref="Profile" />
	public class StrategyProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StrategyProfile"/> class.
		/// </summary>
		public StrategyProfile()
		{
			CreateMap<IStrategy, StrategyViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
				.ForMember(t => t.Name, source => source.MapFrom(s => s.Name))
				.ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
				.ForMember(t => t.Image, source => source.MapFrom(s => s.Image));

			CreateMap<IStrategy, SelectionViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.Text, source => source.MapFrom(s => s.Name));
		}
	}
}