using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionBuyingOrderAddedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the order date
        /// </summary>
        public DateTime OrderDate { get; private set; }

        /// <summary>
        /// Gets the amount of shares
        /// </summary>
        public decimal Shares { get; private set; }

        /// <summary>
        /// Gets the price per share
        /// </summary>
        public decimal PricePerShare { get; private set; }

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

        /// <summary>
        /// Gets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        public decimal PositionSize { get; private set; }

        /// <summary>
        /// Gets the change risk ratio = TP Points/SL Points
        /// </summary>
        public decimal CRV { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBuyingOrderAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="orderDate">The order date.</param>
        /// <param name="units">The units.</param>
        /// <param name="pricePerUnit">The price per unit.</param>
        /// <param name="orderCosts">The order costs.</param>
        /// <param name="description">The description.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="image">The image.</param>
        /// <param name="initialSL">The initial sl.</param>
        /// <param name="initialTP">The initial tp.</param>
        /// <param name="stockId">The stock identifier.</param>
        /// <param name="strategyId">The strategy identifier.</param>
        /// <param name="positionSize">Size of the position.</param>
        /// <param name="crv">The CRV.</param>
        public TransactionBuyingOrderAddedEvent(Guid id, Type aggregateType,
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
            Guid strategyId,
            decimal positionSize,
            decimal crv)
            : base(id, aggregateType)
        {
            OrderDate = orderDate;
            Shares = units;
            PricePerShare = pricePerUnit;
            OrderCosts = orderCosts;
            Description = description;
            Tag = tag;
            Image = image;
            InitialSL = initialSL;
            InitialTP = initialTP;
            StockId = stockId;
            StrategyId = strategyId;
            PositionSize = positionSize;
            CRV = crv;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBuyingOrderAddedEvent"/> class.
        /// </summary>
        protected TransactionBuyingOrderAddedEvent()
        {
            
        }
    }
}