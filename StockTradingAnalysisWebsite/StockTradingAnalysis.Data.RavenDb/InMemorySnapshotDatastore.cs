using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using StockTradingAnalysis.Data.RavenDb.Extensions;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using IDocumentStore = Raven.Client.IDocumentStore;
using IndexCreation = Raven.Client.Indexes.IndexCreation;

namespace StockTradingAnalysis.Data.RavenDb
{
	/// <summary>
	/// Datastore the entry class for the raven db database as an in-memory datastore
	/// </summary>
	public class InMemorySnapshotDatastore : ISnapshotDatastore
	{
		private static IDocumentStore _store;

		/// <summary>
		/// Initializes this object
		/// </summary>
		public InMemorySnapshotDatastore()
		{
			var store = new EmbeddableDocumentStore()
			{
				RunInMemory = true
			};

			store.Configuration.Storage.Voron.AllowOn32Bits = true;

			Initialize(store);
		}

		/// <summary>
		/// Initializes the database
		/// </summary>
		/// <param name="store">The documentstore with connection parameters already configured</param>
		private static void Initialize(IDocumentStore store)
		{
			_store = store;

			//Indexes
			_store.Conventions.FindTypeTagName = type =>
			{
				if (typeof(SnapshotBase).IsAssignableFrom(type))
					return "Snapshots";

				return DocumentConvention.DefaultTypeTagName(type);
			};

			_store.Initialize();

			IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), _store);
		}

		/// <summary>
		/// Stores one item
		/// </summary>
		/// <param name="item">The item</param>
		public void Store(SnapshotBase item)
		{
			using (var session = _store.OpenSession())
			{
				session.Store(item);
				session.SaveChanges();
			}
		}

		/// <summary>
		/// Stores a list of items
		/// </summary>
		/// <param name="items">The items</param>
		public void Store(IEnumerable<SnapshotBase> items)
		{
			using (var session = _store.BulkInsert())
			{
				foreach (var item in items)
				{
					session.Store(item);
				}
			}
		}

		/// <summary>
		/// Selects all items
		/// </summary>
		/// <returns>All items</returns>
		public IEnumerable<SnapshotBase> Select()
		{
			using (var session = _store.OpenSession())
			{
				return session.GetAll<SnapshotBase>("SnapshotIndex");
			}
		}

		/// <summary>
		/// Selects all items with the given aggregateId
		/// </summary>
		/// <returns>All items</returns>
		public IEnumerable<SnapshotBase> Select(Guid aggregateId)
		{
			using (var session = _store.OpenSession())
			{
				return session.GetAll<SnapshotBase>("SnapshotIndex", f => f.AggregateId.Equals(aggregateId));
			}
		}

		/// <summary>
		/// Deletes all documents
		/// </summary>
		public void DeleteAll()
		{
			var staleIndexesWaitAction = new Action(
				delegate
				{
					while (_store.DatabaseCommands.GetStatistics().StaleIndexes.Length != 0)
					{
						Thread.Sleep(100);
					}
				});

			staleIndexesWaitAction.Invoke();
			_store.DatabaseCommands.DeleteByIndex("SnapshotIndex", new IndexQuery());
			staleIndexesWaitAction.Invoke();
		}
	}
}