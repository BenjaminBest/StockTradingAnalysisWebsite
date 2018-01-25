using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class TransactionDividendOrderAddedEvent : DomainEvent
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
        /// Gets the price per unit
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
        /// Gets the underlying stock
        /// </summary>
        public Guid StockId { get; private set; }

        /// <summary>
        /// Gets the taxes paid
        /// </summary>
        public decimal Taxes { get; private set; }

        /// <summary>
        /// Gets the position size (amount of money put in the trade, without transaction costs)
        /// </summary>
        public decimal PositionSize { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDividendOrderAddedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        /// <param name="orderDate">The order date.</param>
        /// <param name="shares">The shares.</param>
        /// <param name="pricePerShare">The price per share.</param>
        /// <param name="orderCosts">The order costs.</param>
        /// <param name="description">The description.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="image">The image.</param>
        /// <param name="stockId">The stock identifier.</param>
        /// <param name="taxes">The taxes.</param>
        /// <param name="positionSize">Size of the position.</param>
        public TransactionDividendOrderAddedEvent(Guid id, Type aggregateType,
            DateTime orderDate,
            decimal shares,
            decimal pricePerShare,
            decimal orderCosts,
            string description,
            string tag,
            IImage image,
            Guid stockId,
            decimal taxes,
            decimal positionSize)
            : base(id, aggregateType)
        {
            OrderDate = orderDate;
            Shares = shares;
            PricePerShare = pricePerShare;
            OrderCosts = orderCosts;
            Description = description;
            Tag = tag;
            Image = image;
            StockId = stockId;
            Taxes = taxes;
            PositionSize = positionSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDividendOrderAddedEvent"/> class.
        /// </summary>
        protected TransactionDividendOrderAddedEvent()
        {

        }
    }
}