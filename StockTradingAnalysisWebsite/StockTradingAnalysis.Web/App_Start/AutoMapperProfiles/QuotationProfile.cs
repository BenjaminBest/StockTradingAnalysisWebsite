using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    public class QuotationProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<IQuotation, QuoteViewModel>()
                .ForMember(t => t.Date, source => source.MapFrom(s => s.Date))
                .ForMember(t => t.Open, source => source.MapFrom(s => s.Open))
                .ForMember(t => t.Close, source => source.MapFrom(s => s.Close))
                .ForMember(t => t.High, source => source.MapFrom(s => s.High))
                .ForMember(t => t.Low, source => source.MapFrom(s => s.Low));
        }
    }
}