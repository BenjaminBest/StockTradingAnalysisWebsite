using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    /// <summary>
    /// The TransactionSellCommand is used when a sell transaction should be executed.
    /// </summary>
    /// <seealso cref="Command" />
    public class TransactionSellCommand : Command
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
        /// Gets the underlying stock
        /// </summary>
        public Guid StockId { get; private set; }

        /// <summary>
        /// Gets the taxes paid
        /// </summary>
        public decimal Taxes { get; private set; }

        /// <summary>
        /// Gets the minimum quote during trade
        /// </summary>
        public decimal? MAE { get; private set; }

        /// <summary>
        /// Gets the maximum quote during trade
        /// </summary>
        public decimal? MFE { get; private set; }

        /// <summary>
        /// Gets the feedbacks
        /// </summary>
        public IEnumerable<Guid> Feedback { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSellCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        /// <param name="orderDate">The order date.</param>
        /// <param name="units">The units.</param>
        /// <param name="pricePerUnit">The price per unit.</param>
        /// <param name="orderCosts">The order costs.</param>
        /// <param name="description">The description.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="image">The image.</param>
        /// <param name="stockId">The stock identifier.</param>
        /// <param name="taxes">The taxes.</param>
        /// <param name="mae">The mae.</param>
        /// <param name="mfe">The mfe.</param>
        /// <param name="feedback">The feedback.</param>
        public TransactionSellCommand(Guid id, int version,
            DateTime orderDate,
            decimal units,
            decimal pricePerUnit,
            decimal orderCosts,
            string description,
            string tag,
            IImage image,
            Guid stockId,
            decimal taxes,
            decimal? mae,
            decimal? mfe,
            IEnumerable<Guid> feedback)
            : base(version, id)
        {
            OrderDate = orderDate;
            Shares = units;
            PricePerShare = pricePerUnit;
            OrderCosts = orderCosts;
            Description = description;
            Tag = tag;
            Image = image;
            StockId = stockId;
            Taxes = taxes;
            MAE = mae;
            MFE = mfe;
            Feedback = feedback.ToList();
        }
    }
}