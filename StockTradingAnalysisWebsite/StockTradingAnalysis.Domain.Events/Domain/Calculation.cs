using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    public class Calculation : ICalculation
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">The id of the calculation</param>
        public Calculation(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets/sets the id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the version
        /// </summary>
        public int OriginalVersion { get; set; }

        /// <summary>
        /// Gets/sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the wkn
        /// </summary>
        public string Wkn { get; set; }

        /// <summary>
        /// Gets/sets the multiplier
        /// </summary>
        public decimal Multiplier { get; set; }

        /// <summary>
        /// Gets/sets the strike price if selling
        /// </summary>
        public decimal? StrikePrice { get; set; }

        /// <summary>
        /// Gets/sets the underlying
        /// </summary>
        public string Underlying { get; set; }

        /// <summary>
        /// Gets/sets the initial stop loss
        /// </summary>
        public decimal InitialSl { get; set; }

        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        public decimal InitialTp { get; set; }

        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        public decimal PricePerUnit { get; set; }

        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        public decimal OrderCosts { get; set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        public decimal Units { get; set; }

        /// <summary>
        /// Gets/sets if its about selling or buying
        /// </summary>
        public bool IsLong { get; set; }
    }
}