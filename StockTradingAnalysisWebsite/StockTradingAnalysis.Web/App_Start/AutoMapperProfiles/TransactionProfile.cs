using AutoMapper;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.AutoMapperProfiles
{
    public class TransactionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ITransaction, TransactionViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.OrderCosts, source => source.MapFrom(s => s.OrderCosts))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.Tag, source => source.MapFrom(s => s.Tag))
                .ForMember(t => t.Image, source => source.MapFrom(s => s.Image))
                .ForMember(t => t.Stock, source => source.MapFrom(s => s.Stock))
                .ForMember(t => t.Action, source => source.MapFrom(s => s.Action));

            Mapper.CreateMap<IBuyingTransaction, TransactionBuyingViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.OrderCosts, source => source.MapFrom(s => s.OrderCosts))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.InitialSL, source => source.MapFrom(s => s.InitialSL))
                .ForMember(t => t.InitialTP, source => source.MapFrom(s => s.InitialTP))
                .ForMember(t => t.Tag, source => source.MapFrom(s => s.Tag))
                .ForMember(t => t.Image, source => source.MapFrom(s => s.Image))
                .ForMember(t => t.Stock, source => source.MapFrom(s => s.Stock))
                .ForMember(t => t.Strategy, source => source.MapFrom(s => s.Strategy));

            Mapper.CreateMap<ISellingTransaction, TransactionSellingViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.OrderCosts, source => source.MapFrom(s => s.OrderCosts))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.Taxes, source => source.MapFrom(s => s.Taxes))
                .ForMember(t => t.Tag, source => source.MapFrom(s => s.Tag))
                .ForMember(t => t.MAE, source => source.MapFrom(s => s.MAE))
                .ForMember(t => t.MFE, source => source.MapFrom(s => s.MFE))
                .ForMember(t => t.Image, source => source.MapFrom(s => s.Image))
                .ForMember(t => t.Stock, source => source.MapFrom(s => s.Stock))
                .ForMember(t => t.Feedback, source => source.MapFrom(s => s.Feedback));

            Mapper.CreateMap<IDividendTransaction, TransactionDividendViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.OrderCosts, source => source.MapFrom(s => s.OrderCosts))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.Taxes, source => source.MapFrom(s => s.Taxes))
                .ForMember(t => t.Tag, source => source.MapFrom(s => s.Tag))
                .ForMember(t => t.Image, source => source.MapFrom(s => s.Image))
                .ForMember(t => t.Stock, source => source.MapFrom(s => s.Stock));

            Mapper.CreateMap<ITransactionPerformance, TransactionPerformanceViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.ProfitAbsolute, source => source.MapFrom(s => s.ProfitAbsolute))
                .ForMember(t => t.ProfitPercentage, source => source.MapFrom(s => s.ProfitPercentage))
                .ForMember(t => t.ProfitMade, source => source.MapFrom(s => s.ProfitMade))
                .ForMember(t => t.HoldingPeriod, source => source.MapFrom(s => s.HoldingPeriod))
                .ForMember(t => t.EntryEfficiency, source => source.MapFrom(s => s.EntryEfficiency))
                .ForMember(t => t.ExitEfficiency, source => source.MapFrom(s => s.ExitEfficiency))
                .ForMember(t => t.R, source => source.MapFrom(s => s.R))
                .ForMember(t => t.MAEAbsolute, source => source.MapFrom(s => s.MAEAbsolute))
                .ForMember(t => t.MFEAbsolute, source => source.MapFrom(s => s.MFEAbsolute));

            Mapper.CreateMap<ITransaction, TransactionIndexViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.OrderCosts, source => source.MapFrom(s => s.OrderCosts))
                .ForMember(t => t.Description, source => source.MapFrom(s => s.Description))
                .ForMember(t => t.Tag, source => source.MapFrom(s => s.Tag))
                .ForMember(t => t.Image, source => source.MapFrom(s => s.Image))
                .ForMember(t => t.Stock, source => source.MapFrom(s => s.Stock))
                .ForMember(t => t.Action, source => source.MapFrom(s => s.Action))
                .ForMember(t => t.PositionSize, source => source.MapFrom(s => s.PositionSize))
                .ForMember(t => t.AccountBalance, source => source.Ignore());

            Mapper.CreateMap<ISplitTransaction, TransactionSplitViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OriginalVersion, source => source.MapFrom(s => s.OriginalVersion))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.Stock, source => source.MapFrom(s => s.Stock));

            Mapper.CreateMap<ITransaction, TransactionHistoryViewModel>()
                .ForMember(t => t.Id, source => source.MapFrom(s => s.Id))
                .ForMember(t => t.OrderDate, source => source.MapFrom(s => s.OrderDate))
                .ForMember(t => t.Units, source => source.MapFrom(s => s.Shares))
                .ForMember(t => t.PricePerUnit, source => source.MapFrom(s => s.PricePerShare))
                .ForMember(t => t.PositionSize, source => source.MapFrom(s => s.PositionSize))
                .ForMember(t => t.Action, source => source.MapFrom(s => s.Action));
        }
    }
}