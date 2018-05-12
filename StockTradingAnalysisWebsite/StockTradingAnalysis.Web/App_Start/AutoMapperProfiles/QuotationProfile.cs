using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    /// <summary>
    /// The QuotationProfile contains the auto mapper configration for <see cref="IQuotation"/>
    /// </summary>
    /// <seealso cref="Profile" />
    public class QuotationProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
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