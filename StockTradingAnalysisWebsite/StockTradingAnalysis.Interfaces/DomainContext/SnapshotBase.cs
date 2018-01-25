using System;

namespace StockTradingAnalysis.Interfaces.DomainContext
{
    /// <summary>
    /// Snapshotbase defines the base class for all aggregate-snapshots (each per aggregate)
    /// </summary>
    public abstract class SnapshotBase
    {
        /// <summary>
        /// Gets the id of the snapshot
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the id of the aggregate
        /// </summary>
        public Guid AggregateId { get; protected set; }

        /// <summary>
        /// Gets or sets the version of the aggregate
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotBase"/> class.
        /// </summary>
        protected SnapshotBase()
        {
            Id = Guid.NewGuid();
        }
    }
}