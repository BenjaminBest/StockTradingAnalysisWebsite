using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Imports.Newtonsoft.Json;
using StockTradingAnalysis.Data.RavenDb.Extensions;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace StockTradingAnalysis.Data.RavenDb
{
    /// <summary>
    /// Datastore the entry class for the raven db database
    /// </summary>
    public class SnapshotDatastore : ISnapshotDatastore
    {
        private static IDocumentStore _store;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="connectionName">Name of the connection string</param>
        /// <param name="typeConverters">Type converters</param>
        public SnapshotDatastore(string connectionName, IEnumerable<JsonConverter> typeConverters)
        {
            Initialize(new DocumentStore() { ConnectionStringName = connectionName }, typeConverters);
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="port">Port</param>
        /// <param name="database">Database</param>
        /// <param name="typeConverters">Type converters</param>
        public SnapshotDatastore(string url, string port, string database, IEnumerable<JsonConverter> typeConverters)
        {
            Initialize(new DocumentStore() { Url = $"{url}:{port}", DefaultDatabase = database }, typeConverters);
        }

        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <param name="store">The documentstore with connection parameters already configured</param>
        /// <param name="converters">Custom type converters</param>
        private static void Initialize(IDocumentStore store, IEnumerable<JsonConverter> converters)
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

            //Register custom type converter
            store.Conventions.CustomizeJsonSerializer = serializer =>
            {
                foreach (var converter in converters)
                {
                    serializer.Converters.Add(converter);
                }
            };

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
                session.Advanced.MaxNumberOfRequestsPerSession = 1000; //TODO: Make configurable

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
                session.Advanced.MaxNumberOfRequestsPerSession = 1000; //TODO: Make configurable

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