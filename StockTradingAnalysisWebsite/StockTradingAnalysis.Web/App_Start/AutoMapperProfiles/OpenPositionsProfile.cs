using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    /// <summary>
    /// The OpenPositionsProfile contains the automapper configuration for <see cref="IDetailedOpenPosition"/> and
    /// <see cref="IDetailedOpenPositionOverview"/>.
    /// </summary>
    /// <seealso cref="Profile" />
    public class OpenPositionsProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Mapper.CreateMap<IDetailedOpenPositionOverview, OpenPositionsViewModel>()
                .ForMember(t => t.OpenPositions, source => source.MapFrom(s => s.OpenPositions));

            Mapper.CreateMap<IDetailedOpenPosition, OpenPositionViewModel>()
                .ForMember(t => t.StockId, source => source.MapFrom(s => s.Stock.Id))
                .ForMember(t => t.StockName, source => source.MapFrom(s => s.Stock.Name))
                .ForMember(t => t.AveragePricePerShare, source => source.MapFrom(s => s.AveragePricePerShare))
                .ForMember(t => t.FirstOrderDate, source => source.MapFrom(s => s.FirstOrderDate))
                .ForMember(t => t.PositionSize, source => source.MapFrom(s => s.PositionSize))
                .ForMember(t => t.Shares, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PositionSize, source => source.MapFrom(s => s.PositionSize))
                .ForMember(t => t.CurrentQuotation, source => source.MapFrom(s => s.CurrentQuotation))
                .ForMember(t => t.Profit, source => source.MapFrom(s => s.Profit));

            Mapper.CreateMap<IDetailedOpenPositionSummary, OpenPositionSummaryViewModel>()
                .ForMember(t => t.PositionSize, source => source.MapFrom(s => s.PositionSize))
                .ForMember(t => t.Profit, source => source.MapFrom(s => s.Profit));

            Mapper.CreateMap<IProfit, ProfitViewModel>()
                .ForMember(t => t.AbsoluteProfit, source => source.MapFrom(s => s.ProfitAbsolute))
                .ForMember(t => t.PercentageProfit, source => source.MapFrom(s => s.ProfitPercentage));
        }
    }
}