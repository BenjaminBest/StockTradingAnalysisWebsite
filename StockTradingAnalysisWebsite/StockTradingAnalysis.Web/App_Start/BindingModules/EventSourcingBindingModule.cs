using Ninject.Extensions.Conventions;
using Ninject.Modules;
using StockTradingAnalysis.Domain.Events.Aggregates;
using StockTradingAnalysis.EventSourcing.DomainContext;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Web.BindingModules
{
    public class EventSourcingBindingModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            //Event sourcing
            Bind<IEventBus>().To<EventBus>().InSingletonScope();
            Bind<IEventStore>().To<EventStore>().InSingletonScope();

            Bind<IPersistentEventStore>().To<DocumentDatabaseEventStore>().WhenInjectedExactlyInto<CachedDocumentDatabaseEventStore>().InSingletonScope();
            Bind<IPersistentEventStore>().To<CachedDocumentDatabaseEventStore>().InSingletonScope();
            Bind<ISnapshotStore>().To<SnapshotStore>().InSingletonScope();
            Bind<IPersistentSnapshotStore>().To<DocumentDatabaseSnapshotStore>().WhenInjectedExactlyInto<CachedDocumentDatabaseSnapshotStore>().InSingletonScope();
            Bind<IPersistentSnapshotStore>().To<CachedDocumentDatabaseSnapshotStore>().InSingletonScope();

            Bind<IDocumentStoreCache<IDomainEvent>>().To<DocumentEventStoreSlidingCache>().InSingletonScope().WithConstructorArgument("amountOfAggregatesToCache", 100);
            Bind<IDocumentStoreCache<SnapshotBase>>().To<DocumentSnapshotStoreSlidingCache>().InSingletonScope().WithConstructorArgument("amountOfAggregatesToCache", 100);

            //Bind<IPersistentEventStore>().To<InMemoryEventStore>().InSingletonScope();
            //Bind<IPersistentSnapshotStore>().To<InMemorySnapshotStore>().InSingletonScope();

            Bind<ISnapshotProcessor>().To<SnapshotProcessor>().WithConstructorArgument("snapshotThreshold", 1000);
            Bind<IEventStoreInitializer>().To<EventStoreInitializer>().InSingletonScope();

            Kernel.Bind<ISnapshotableRepository>().To<AggregateRepository<StockAggregate>>();

            Kernel.Bind(i => i
                .FromAssembliesMatching("StockTradingAnalysis.*.dll")
                .SelectAllClasses()
                .InheritedFrom(typeof(IEventHandler<>))
                .BindAllInterfaces());

            //TODO: Make configurable
            //Kernel.Bind<IDatastore>().To<InMemoryDatastore>();

            //RavenDB
            //Kernel.Bind<IEventDatastore>()
            //    .To<Data.RavenDb.EventDatastore>()
            //    .InSingletonScope()
            //    .WithConstructorArgument("connectionName", "StockTradingAnalysis_RavenDB");

            //Kernel.Bind<ISnapshotDatastore>()
            //    .To<Data.RavenDb.SnapshotDatastore>()
            //    .InSingletonScope()
            //    .WithConstructorArgument("connectionName", "StockTradingAnalysis_RavenDB");

            //MSSQL
            Kernel.Bind<IEventDatastore>()
                .To<Data.MSSQL.EventDatastore>()
                .InSingletonScope()
                .WithConstructorArgument("connectionName", "StockTradingAnalysis_MSSQL")
                .WithConstructorArgument("tableName", "[dbo].[EventDataStore]");

            Kernel.Bind<ISnapshotDatastore>()
                .To<Data.MSSQL.SnapShotDatastore>()
                .InSingletonScope()
                .WithConstructorArgument("connectionName", "StockTradingAnalysis_MSSQL")
                .WithConstructorArgument("tableName", "[dbo].[SnapshotDataStore]");
        }
    }
}