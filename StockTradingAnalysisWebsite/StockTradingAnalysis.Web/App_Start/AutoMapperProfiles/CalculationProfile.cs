using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    /// <summary>
    /// The CalculationProfile contains the mapper configuration <see cref="ICalculation"/>.
    /// </summary>
    /// <seealso cref="Profile" />
    public class CalculationProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Mapper.CreateMap<ICalculation, CalculationViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.InitialSl, source => source.MapFrom(s => s.InitialSl))
                .ForMember(t => t.InitialTp, source => source.MapFrom(s => s.InitialTp))
                .ForMember(t => t.IsLong, source => source.MapFrom(s => s.IsLong))
                .ForMember(t => t.Multiplier, source => source.MapFrom(s => s.Multiplier))
                .ForMember(t => t.Name, source => source.MapFrom(s => s.Name))
                .ForMember(t => t.OrderCosts, source => source.MapFrom(s => s.OrderCosts))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerUnit))
                .ForMember(t => t.StrikePrice, source => source.MapFrom(s => s.StrikePrice))
                .ForMember(t => t.Underlying, source => source.MapFrom(s => s.Underlying))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Units))
                .ForMember(t => t.Wkn, source => source.MapFrom(s => s.Wkn));

            Mapper.CreateMap<ICalculationQuotation, CalculationQuotationViewModel>()
                .ForMember(t => t.IsBreakEven, source => source.MapFrom(s => s.IsBreakEven))
                .ForMember(t => t.IsBuy, source => source.MapFrom(s => s.IsBuy))
                .ForMember(t => t.IsCurrentQuotation, source => source.MapFrom(s => s.IsCurrentQuotation))
                .ForMember(t => t.IsStoppLoss, source => source.MapFrom(s => s.IsStoppLoss))
                .ForMember(t => t.IsTakeProfit, source => source.MapFrom(s => s.IsTakeProfit))
                .ForMember(t => t.PlAbsolute, source => source.MapFrom(s => s.PlAbsolute))
                .ForMember(t => t.PlPercentage, source => source.MapFrom(s => s.PlPercentage))
                .ForMember(t => t.Quotation, source => source.MapFrom(s => s.Quotation))
                .ForMember(t => t.QuotationUnderlying, source => source.MapFrom(s => s.QuotationUnderlying));

            Mapper.CreateMap<ICalculationResult, CalculationResultViewModel>()
                .ForMember(t => t.Wkn, source => source.MapFrom(s => s.Wkn))
                .ForMember(t => t.Atr, source => source.MapFrom(s => s.Atr))
                .ForMember(t => t.BreakEven, source => source.MapFrom(s => s.BreakEven))
                .ForMember(t => t.Buy, source => source.MapFrom(s => s.Buy))
                .ForMember(t => t.Crv, source => source.MapFrom(s => s.Crv))
                .ForMember(t => t.FirstPart, source => source.MapFrom(s => s.FirstPart))
                .ForMember(t => t.LastPart, source => source.MapFrom(s => s.LastPart))
                .ForMember(t => t.IsUnderlyingUsed, source => source.MapFrom(s => s.IsUnderlyingUsed))
                .ForMember(t => t.PointsToBe, source => source.MapFrom(s => s.PointsToBe))
                .ForMember(t => t.PointsToSl, source => source.MapFrom(s => s.PointsToSl))
                .ForMember(t => t.PointsToTp, source => source.MapFrom(s => s.PointsToTp))
                .ForMember(t => t.BreakEven, source => source.MapFrom(s => s.BreakEven))
                .ForMember(t => t.CurrentQuotation, source => source.MapFrom(s => s.CurrentQuotation))
                .ForMember(t => t.Quotation, source => source.MapFrom(s => s.Quotation))
                .ForMember(t => t.PositionSize, source => source.MapFrom(s => s.PositionSize))
                .ForMember(t => t.TakeProfit, source => source.MapFrom(s => s.TakeProfit))
                .ForMember(t => t.StoppLoss, source => source.MapFrom(s => s.StoppLoss));
        }
    }
}