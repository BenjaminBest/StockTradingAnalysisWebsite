using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
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