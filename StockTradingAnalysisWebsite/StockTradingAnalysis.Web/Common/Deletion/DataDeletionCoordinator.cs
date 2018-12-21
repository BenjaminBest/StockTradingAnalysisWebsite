using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.ReadModel;


namespace StockTradingAnalysis.Web.Common.Deletion
{
	/// <summary>
	/// The ModelRepositoryDeletionCoordinator searches for all stores and repositories that supports deletion and asks them to delete all data
	/// </summary>
	/// <seealso cref="ModelRepositoryDeletionCoordinator" />
	public class ModelRepositoryDeletionCoordinator : IModelRepositoryDeletionCoordinator
	{
		/// <summary>
		/// The event datastore
		/// </summary>
		private readonly IEventDatastore _eventDatastore;

		/// <summary>
		/// The snapshot datastore
		/// </summary>
		private readonly ISnapshotDatastore _snapshotDatastore;

		/// <summary>
		/// The event document store cache
		/// </summary>
		private readonly IDocumentStoreCache<IDomainEvent> _eventDocumentStoreCache;

		/// <summary>
		/// The snapshot document store cache
		/// </summary>
		private readonly IDocumentStoreCache<SnapshotBase> _snapshotDocumentStoreCache;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelRepositoryDeletionCoordinator" /> class.
		/// </summary>
		/// <param name="eventDatastore">The event datastore.</param>
		/// <param name="snapshotDatastore">The snapshot datastore.</param>
		/// <param name="eventDocumentStoreCache">The event document store cache.</param>
		/// <param name="snapshotDocumentStoreCache">The snapshot document store cache.</param>
		public ModelRepositoryDeletionCoordinator(
			IEventDatastore eventDatastore,
			ISnapshotDatastore snapshotDatastore,
			IDocumentStoreCache<IDomainEvent> eventDocumentStoreCache,
			IDocumentStoreCache<SnapshotBase> snapshotDocumentStoreCache)
		{
			_eventDatastore = eventDatastore;
			_snapshotDatastore = snapshotDatastore;
			_eventDocumentStoreCache = eventDocumentStoreCache;
			_snapshotDocumentStoreCache = snapshotDocumentStoreCache;
		}

		/// <summary>
		/// Deletes all.data in the persistent store and caches
		/// </summary>
		public void DeleteAll()
		{
			_eventDatastore.DeleteAll();
			_snapshotDatastore.DeleteAll();
			_eventDocumentStoreCache.DeleteAll();
			_snapshotDocumentStoreCache.DeleteAll();

			var types = DependencyResolver.GetServices<ISupportsDataDeletion>();
			foreach (var type in types)
			{
				type.DeleteAll();
			}
		}
	}
}