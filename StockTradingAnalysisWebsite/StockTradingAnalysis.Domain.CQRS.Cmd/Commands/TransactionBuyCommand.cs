using System;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class TransactionBuyCommand : Command
    {
        /// <summary>
        /// Gets the order date
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Gets the amount of units
        /// </summary>
        public decimal Units { get; private set; }

        /// <summary>
        /// Gets the price per unit
        /// </summary>
        public decimal PricePerUnit { get; private set; }

        /// <summary>
        /// Gets the order costs
        /// </summary>
        public decimal OrderCosts { get; private set; }

        /// <summary>
        /// Gets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the tag
        /// </summary>
        public string Tag { get; private set; }

        /// <summary>
        /// Gets the image
        /// </summary>
        public IImage Image { get; private set; }

        /// <summary>
        /// Gets the initial stop loss
        /// </summary>
        public decimal InitialSL { get; private set; }

        /// <summary>
        /// Gets the initial take profit
        /// </summary>
        public decimal InitialTP { get; private set; }

        /// <summary>
        /// Gets the underlying stock
        /// </summary>
        public Guid StockId { get; private set; }

        /// <summary>
        /// Gets the strategy used
        /// </summary>
        public Guid StrategyId { get; private set; }

        public TransactionBuyCommand(Guid id, int version,
            DateTime orderDate,
            decimal units,
            decimal pricePerUnit,
            decimal orderCosts,
            string description,
            string tag,
            IImage image,
            decimal initialSL,
            decimal initialTP,
            Guid stockId,
            Guid strategyId)
            : base(version, id)
        {
            OrderDate = orderDate;
            Units = units;
            PricePerUnit = pricePerUnit;
            OrderCosts = orderCosts;
            Description = description;
            Tag = tag;
            Image = image;
            StockId = stockId;
            InitialSL = initialSL;
            InitialTP = initialTP;
            StrategyId = strategyId;
        }
    }
}