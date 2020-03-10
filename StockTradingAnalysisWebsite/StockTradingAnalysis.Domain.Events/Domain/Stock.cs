using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a stock
    /// </summary>
    [Serializable]
    public class Stock : IStock
    {
        /// <summary>
        /// List of quotations for this stock
        /// </summary>
        private readonly IDictionary<DateTime, IQuotation> _quotations;

        /// <summary>
        /// List of quotations for this stock
        /// </summary>
        public IEnumerable<IQuotation> Quotations => _quotations.Values;

        /// <summary>
        /// Gets/sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the version
        /// </summary>
        public int OriginalVersion { get; set; }

        /// <summary>
        /// Returns a combined display text which includes the name and wkn
        /// </summary>
        public string StocksDescription => $"{Name} ({Wkn} [{Type}])";

        /// <summary>
        /// Returns a combined display text which includes the name and wkn
        /// </summary>
        public string StocksShortDescription => $"{Name} ({Wkn})";

        /// <summary>
        /// Gets/sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the wkn
        /// </summary>
        public string Wkn { get; set; }

        /// <summary>
        /// Gets/sets the type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets/sets if this stock is for buying or selling
        /// </summary>
        public string LongShort { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">The id of a stock</param>
        public Stock(Guid id)
        {
            Id = id;
            _quotations = new Dictionary<DateTime, IQuotation>();
        }

        /// <summary>
        /// Adds a stock price
        /// </summary>
        /// <remarks>Checks if quotation already exists, but has changed</remarks>
        public void AddQuotation(IQuotation quotation)
        {
            if (_quotations.ContainsKey(quotation.Date))
            {
                var existentQuotation = _quotations[quotation.Date];

                if (existentQuotation.Open == quotation.Open &&
                    existentQuotation.Close == quotation.Close &&
                    existentQuotation.High == quotation.High &&
                    existentQuotation.Low == quotation.Low)
                    return;

                _quotations.Remove(quotation.Date);
                _quotations.Add(quotation.Date, quotation);
            }
            else
            {
                _quotations.Add(quotation.Date, quotation);
            }
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}