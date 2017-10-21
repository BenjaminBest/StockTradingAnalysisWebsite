using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;
using System;
using System.Collections.Generic;

namespace StockTradingAnalysis.Domain.Events.Snapshots
{
    public class StockAggregateSnapshot : SnapshotBase
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets if the stock is used when buying or selling
        /// </summary>
        public string LongShort { get; private set; }

        /// <summary>
        /// List of quotations for this stock
        /// </summary>
        public HashSet<IQuotation> Quotations { get; private set; }

        /// <summary>
        /// Initializes this snapshot with the given values
        /// </summary>
        /// <param name="aggregateId">Aggregate Id</param>
        /// <param name="version">Version</param>
        /// <param name="name">Name</param>
        /// <param name="wkn">Wkn</param>
        /// <param name="type">Type</param>
        /// <param name="longShort">Long or short</param>
        /// <param name="quotations">Quotations</param>
        public StockAggregateSnapshot(Guid aggregateId, int version, string name, string wkn, string type, string longShort, HashSet<IQuotation> quotations)
        {
            Name = name;
            Wkn = wkn;
            Type = type;
            LongShort = longShort;
            Quotations = quotations;
            AggregateId = aggregateId;
            Version = version;
        }
    }
}
