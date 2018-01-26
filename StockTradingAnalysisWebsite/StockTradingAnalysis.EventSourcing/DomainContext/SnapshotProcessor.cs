using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.EventSourcing.DomainContext
{
    /// <summary>
    /// The interface ISnapshotProcessor defines an snapshot-creator which actually creates and saves the snapshot
    /// </summary>
    public class SnapshotProcessor : ISnapshotProcessor
    {
        private readonly ISnapshotStore _snapshotStore;
        private readonly int _snapshotThreshold;
        private readonly IPerformanceMeasurementService _performanceMeasurementService;
        private readonly Dictionary<Type, ISnapshotableRepository> _repositories;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="snapshotStore">The snapshot store</param>
        /// <param name="repositories">A list of all aggregate repositories</param>
        /// <param name="snapshotThreshold">The Threshold above which should a snapshot be created</param>
        /// <param name="performanceMeasurementService">The performance measurement service</param>
        public SnapshotProcessor(
            ISnapshotStore snapshotStore,
            IEnumerable<ISnapshotableRepository> repositories,
            int snapshotThreshold,
            IPerformanceMeasurementService performanceMeasurementService)
        {
            _snapshotStore = snapshotStore;
            _snapshotThreshold = snapshotThreshold;
            _performanceMeasurementService = performanceMeasurementService;

            _repositories = repositories.Where(r => r.IsSnapshotSupported())
                .ToDictionary(r => r.GetOriginatorType(), r => r);
        }

        /// <summary>
        /// Creates a new snapshot for the aggregate of type <paramref name="aggregateType" /> with id
        /// <paramref name="aggregateId" />
        /// </summary>
        /// <param name="aggregateId">The Id of an aggregate</param>
        /// <param name="aggregateType">The type of an aggregate</param>
        public void CreateSnapshot(Guid aggregateId, Type aggregateType)
        {
            //Check if snapshotting is supported
            ISnapshotableRepository repository;
            _repositories.TryGetValue(aggregateType, out repository);

            var originator = repository?.GetOriginator(aggregateId);

            if (originator == null || originator.OriginatorId == Guid.Empty)
                return;

            using (new TimeMeasure(ms => _performanceMeasurementService.CountSnapshot()))
            {
                _snapshotStore.SaveSnapshot(originator);
            }
        }

        /// <summary>
        /// Returns <c>true</c> if a snapshould should be created
        /// </summary>
        /// <param name="aggregateId">The Id of an aggregate</param>
        /// <param name="aggregateType">The type of an aggregate</param>
        /// <param name="aggregateVersion">The current version of the aggregate</param>
        /// <returns><c>True</c> if a snapshould should be created</returns>
        public bool IsSnapshotNeeded(Guid aggregateId, Type aggregateType, int aggregateVersion)
        {
            //Check if snapshotting is supported
            if (!_repositories.ContainsKey(aggregateType) && aggregateVersion == 0)
                return false;

            var snapshot = _snapshotStore.GetSnapshot(aggregateId);
            var lastVersion = snapshot?.Version ?? 0;

            return aggregateVersion - lastVersion >= _snapshotThreshold;

            //return aggregateVersion % _snapshotThreshold == 0;
        }
    }
}