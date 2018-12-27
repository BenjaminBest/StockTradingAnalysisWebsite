using AutoMapper;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.CQRS.Query.Queries;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Web.Models;
using System.Linq;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
	/// <summary>
	/// The StockProfile cotains the automapper configuration for <see cref="IStock"/>.
	/// </summary>
	/// <seealso cref="Profile" />
	public class StockProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StockProfile"/> class.
		/// </summary>
		public StockProfile()
		{
			CreateMap<IStock, StockViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
				.ForMember(t => t.StocksDescription, source => source.MapFrom(s => s.StocksDescription))
				.ForMember(t => t.StocksShortDescription, source => source.MapFrom(s => s.StocksShortDescription))
				.ForMember(t => t.Name, source => source.MapFrom(s => s.Name))
				.ForMember(t => t.Wkn, source => source.MapFrom(s => s.Wkn))
				.ForMember(t => t.Type, source => source.MapFrom(s => s.Type))
				.ForMember(t => t.LongShort, source => source.MapFrom(s => s.LongShort))
				.ForMember(t => t.Performance, source => source.MapFrom(s => ResolvePerformance(s)))
				.ForMember(t => t.TransactionHistory, source => source.MapFrom(s => ResolveTransactionHistory(s)))
				.ForMember(t => t.LastestQuote, source => source.MapFrom(s => ResolveLatestQuote(s)));

			CreateMap<IStock, SelectionViewModel>()
				.ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
				.ForMember(t => t.Text, source => source.MapFrom(s => s.StocksShortDescription));

		}

		/// <summary>
		/// Resolves the performance for the given <paramref name="stock"/>
		/// </summary>
		/// <param name="stock">The stock.</param>
		/// <returns>Performance</returns>
		private decimal ResolvePerformance(IStock stock)
		{
			if (stock == null)
				return default(decimal);

			var modelRepository = DependencyResolver.Current.GetService<IModelReaderRepository<IStockStatistics>>();

			var model = modelRepository.GetById(stock.Id);

			return model?.Performance ?? default(decimal);
		}

		/// <summary>
		/// Resolves the transaction history for the given <paramref name="stock"/>
		/// </summary>
		/// <param name="stock">The stock.</param>
		/// <returns>Transaction history</returns>
		private object ResolveTransactionHistory(IStock stock)
		{
			if (stock == null)
				return Enumerable.Empty<TransactionHistoryViewModel>();

			var queryDispatcher = DependencyResolver.Current.GetService<IQueryDispatcher>();

			var transactions = queryDispatcher.Execute(new TransactionByStockIdQuery(stock.Id));

			return transactions ?? Enumerable.Empty<ITransaction>();
		}

		/// <summary>
		/// Resolves the latest quote for the given <paramref name="stock"/>
		/// </summary>
		/// <param name="stock">The stock.</param>
		/// <returns>Latest quote</returns>
		private IQuotation ResolveLatestQuote(IStock stock)
		{
			if (stock == null)
				return null;

			var queryDispatcher = DependencyResolver.Current.GetService<IQueryDispatcher>();

			var quote = queryDispatcher.Execute(new StockQuotationsLatestByIdQuery(stock.Id));

			return quote;
		}
	}
}