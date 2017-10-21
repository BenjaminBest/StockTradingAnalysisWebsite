using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class CalculationAddCommand : Command
    {
        /// <summary>
        /// Gets/sets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets/sets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        /// <summary>
        /// Gets/sets the multiplier
        /// </summary>
        public decimal Multiplier { get; private set; }

        /// <summary>
        /// Gets/sets the strike price if selling
        /// </summary>
        public decimal? StrikePrice { get; private set; }

        /// <summary>
        /// Gets/sets the underlying
        /// </summary>
        public string Underlying { get; private set; }

        /// <summary>
        /// Gets/sets the initial stop loss
        /// </summary>
        public decimal InitialSl { get; private set; }

        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        public decimal InitialTp { get; private set; }

        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        public decimal PricePerUnit { get; private set; }

        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        public decimal OrderCosts { get; private set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        public decimal Units { get; private set; }

        /// <summary>
        /// Gets/sets if its about selling or buying
        /// </summary>
        public bool IsLong { get; private set; }

        public CalculationAddCommand(Guid id, int version, string name, string wkn, decimal multiplier,
            decimal? strikePrice, string underlying, decimal initialSl, decimal initialTp, decimal pricePerUnit,
            decimal orderCosts, string description, decimal units, bool isLong)
            : base(version, id)
        {
            Name = name;
            Wkn = wkn;
            Multiplier = multiplier;
            StrikePrice = strikePrice;
            Underlying = underlying;
            InitialSl = initialSl;
            InitialTp = initialTp;
            PricePerUnit = pricePerUnit;
            OrderCosts = orderCosts;
            Description = description;
            Units = units;
            IsLong = isLong;
        }
    }
}