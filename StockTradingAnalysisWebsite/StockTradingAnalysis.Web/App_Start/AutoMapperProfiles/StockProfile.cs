using System.Web.Mvc;
using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    /// <summary>
    /// The StockProfile cotains the automapper configuration for <see cref="IStock"/>
    /// </summary>
    /// <seealso cref="Profile" />
    public class StockProfile : Profile
    {
        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
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
                .ForMember(t => t.Performance, source => source.ResolveUsing(ResolvePerformance));

            Mapper.CreateMap<IStock, SelectionViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.Text, source => source.MapFrom(s => s.StocksShortDescription));
        }

        /// <summary>
        /// Resolves the performance for the given <paramref name="stock"/>
        /// </summary>
        /// <param name="stock">The stock.</param>
        /// <returns></returns>
        private object ResolvePerformance(IStock stock)
        {
            if (stock == null)
                return null;

            var modelRepository = DependencyResolver.Current.GetService<IModelReaderRepository<IStockStatistics>>();

            var model = modelRepository.GetById(stock.Id);

            return model?.Performance ?? default(decimal);
        }
    }
}