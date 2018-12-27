using Microsoft.Extensions.DependencyInjection;
using StockTradingAnalysis.Data.MSSQL;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.EventSourcing.DomainContext;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services.Core;
using StockTradingAnalysis.Web.Common.Binding;
using StockTradingAnalysis.Web.Common.Interfaces;
using System.Collections.Generic;

namespace StockTradingAnalysis.Web.BindingModules
{
	/// <summary>
	/// Binding module for event-sourcing.
	/// </summary>
	public class EventSourcingBindingModule : IBindingModule
	{
		/// <summary>
		/// Loads the binding configuration.
		/// </summary>
		/// <param name="serviceCollection">The service collection.</param>
		public void Load(IServiceCollection serviceCollection)
		{
			//TODO: Make configurable
			//serviceCollection.AddTransient<IDatastore,InMemoryDatastore>();

			//RavenDB
			//serviceCollection.AddSingleton<IEventDatastore>(s => new Data.RavenDb.EventDatastore("StockTradingAnalysis_RavenDB",
			//	s.GetService<IEnumerable<Raven.Imports.Newtonsoft.Json.JsonConverter>>()));

			//serviceCollection.AddSingleton<ISnapshotDatastore>(s => new Data.RavenDb.SnapshotDatastore("StockTradingAnalysis_RavenDB",
			//	s.GetService<IEnumerable<Raven.Imports.Newtonsoft.Json.JsonConverter>>()));

			//MSSQL
			serviceCollection.AddSingleton<IEventDatastore>(s => new EventDatastore("StockTradingAnalysis_MSSQL",
				"[dbo].[EventDataStore]", s.GetService<IPerformanceMeasurementService>(),
				s.GetService<IConfigurationRegistry>()));

			serviceCollection.AddSingleton<ISnapshotDatastore>(s => new SnapShotDatastore("StockTradingAnalysis_MSSQL",
				"[dbo].[SnapshotDataStore]", s.GetService<IPerformanceMeasurementService>(),
				s.GetService<IConfigurationRegistry>()));


			//Event sourcing
			serviceCollection.AddSingleton<IEventBus, EventBus>();
			serviceCollection.AddSingleton<IEventStore, EventStore>();

			serviceCollection.AddSingleton<IDocumentStoreCache<IDomainEvent>>(s => new DocumentEventStoreSlidingCache(100)); //TODO: Make configurable
			serviceCollection.AddSingleton<IDocumentStoreCache<SnapshotBase>>(s => new DocumentSnapshotStoreSlidingCache(100)); //TODO: Make configurable			

			serviceCollection.AddSingleton<IPersistentEventStore>(s => new CachedDocumentDatabaseEventStore(
				new DocumentDatabaseEventStore(s.GetService<IEventDatastore>()),
					s.GetService<IDocumentStoreCache<IDomainEvent>>(), s.GetService<IPerformanceMeasurementService>()));

			serviceCollection.AddSingleton<ISnapshotStore, SnapshotStore>();
			serviceCollection.AddSingleton<IPersistentSnapshotStore>(s => new CachedDocumentDatabaseSnapshotStore(s.GetService<ISnapshotDatastore>(),
				s.GetService<IDocumentStoreCache<SnapshotBase>>(), s.GetService<IPerformanceMeasurementService>()));

			serviceCollection.AddSingleton<IPersistentSnapshotStore, CachedDocumentDatabaseSnapshotStore>();

			//serviceCollection.AddSingleton<IPersistentEventStore,InMemoryEventStore>();
			//serviceCollection.AddSingleton<IPersistentSnapshotStore,InMemorySnapshotStore>();

			serviceCollection.AddTransient<ISnapshotableRepository, AggregateRepository<StockAggregate>>();

			serviceCollection.AddTransient<ISnapshotProcessor>(s => new SnapshotProcessor(s.GetService<ISnapshotStore>(),
					s.GetService<IEnumerable<ISnapshotableRepository>>(), 1000, //TODO: Make configurable
					s.GetService<IPerformanceMeasurementService>()));

			serviceCollection.AddSingleton<IEventStoreInitializer, EventStoreInitializer>();

			serviceCollection.AddTransientForAllInterfaces(typeof(IEventHandler<>));
		}
	}
}