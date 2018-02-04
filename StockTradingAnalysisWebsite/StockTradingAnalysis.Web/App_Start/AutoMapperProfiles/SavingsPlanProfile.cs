using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    public class SavingsPlanProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ISavingsPlanPeriod, SavingsPlanPeriodViewModel>()
                .ForMember(t => t.SumInpayment, source => source.MapFrom(s => s.SumInpayment))
                .ForMember(t => t.SumCapital, source => source.MapFrom(s => s.SumCapital))
                .ForMember(t => t.SumOrderCosts, source => source.MapFrom(s => s.SumOrderCosts))
                .ForMember(t => t.SumOrderCostsPercentage, source => source.MapFrom(s => s.SumOrderCostsPercentage))
                .ForMember(t => t.PerformanceActualPeriodPercentage, source => source.MapFrom(s => s.PerformanceActualPeriodPercentage))
                .ForMember(t => t.PerformanceOverallPeriodPercentage, source => source.MapFrom(s => s.PerformanceOverallPeriodPercentage))
                .ForMember(t => t.SumOverallDividends, source => source.MapFrom(s => s.SumOverallDividends))
                .ForMember(t => t.SumDividends, source => source.MapFrom(s => s.SumDividends))
                .ForMember(t => t.SumOverallDividendsPercentage, source => source.MapFrom(s => s.SumOverallDividendsPercentage))
                .ForMember(t => t.IsForecast, source => source.MapFrom(s => s.IsForecast))
                .ForMember(t => t.IsCurrentYear, source => source.MapFrom(s => s.IsCurrentYear));

            Mapper.CreateMap<ISavingsPlan, SavingsPlanViewModel>()
                    .ForMember(t => t.Date, source => source.MapFrom(s => s.Date))
                    .ForMember(t => t.Tag, source => source.MapFrom(s => s.Tag))
                    .ForMember(t => t.Periods, source => source.MapFrom(s => s.Periods));
        }
    }
}