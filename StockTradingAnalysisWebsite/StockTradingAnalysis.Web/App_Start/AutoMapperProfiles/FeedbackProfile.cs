using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    public class FeedbackProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<IFeedback, FeedbackViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.Name, source => source.MapFrom(s => s.Name))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description));

            Mapper.CreateMap<IFeedback, SelectionViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.Text, source => source.MapFrom(s => s.Name));

            Mapper.CreateMap<IFeedbackProportion, FeedbackProportionViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.Proportion, source => source.MapFrom(s => s.ProportionPercentage));
        }
    }
}