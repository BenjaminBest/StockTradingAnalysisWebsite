using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    public class StockProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<IStock, StockViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.StocksDescription, source => source.MapFrom(s => s.StocksDescription))
                .ForMember(t => t.StocksShortDescription, source => source.MapFrom(s => s.StocksShortDescription))
                .ForMember(t => t.Name, source => source.MapFrom(s => s.Name))
                .ForMember(t => t.Wkn, source => source.MapFrom(s => s.Wkn))
                .ForMember(t => t.Type, source => source.MapFrom(s => s.Type))
                .ForMember(t => t.LongShort, source => source.MapFrom(s => s.LongShort))
                .ForMember(t => t.Performance, source => source.MapFrom(s => s.Performance));

            Mapper.CreateMap<IStock, SelectionViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.Text, source => source.MapFrom(s => s.StocksShortDescription));
        }
    }
}