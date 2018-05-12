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
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
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