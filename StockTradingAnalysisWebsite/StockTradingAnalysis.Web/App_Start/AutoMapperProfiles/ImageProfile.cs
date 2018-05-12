using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    /// <summary>
    /// The ImageProfile contains the auto mapper configuration for an image, <see cref="IImage"/>.
    /// </summary>
    /// <seealso cref="Profile" />
    public class ImageProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            Mapper.CreateMap<IImage, ImageViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.ContentType, source => source.MapFrom(s => s.ContentType))
                .ForMember(t => t.OriginalName, source => source.MapFrom(s => s.OriginalName))
                .ForMember(t => t.Data, source => source.MapFrom(s => s.Data));
        }
    }
}