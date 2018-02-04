using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Data.MSSQL
{
    /// <summary>
    /// Datastore the entry class for the raven db database
    /// </summary>
    public class SnapShotDatastore : DatastoreBase, ISnapshotDatastore
    {
        /// <summary>
        /// The table name
        /// </summary>
        private readonly string _tableName;

        /// <summary>
        /// The performance measurement service
        /// </summary>
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="connectionName">Name of the connection string</param>
        /// <param name="tableName">Name of the table</param>
        /// <param name="performanceMeasurementService">The performance measurement service.</param>
        /// <param name="configurationRegistry">The configuration registry.</param>
        public SnapShotDatastore(
            string connectionName,
            string tableName,
            IPerformanceMeasurementService performanceMeasurementService,
            IConfigurationRegistry configurationRegistry)
            : base(performanceMeasurementService)
        {
            _tableName = tableName;
            _connectionString = configurationRegistry.GetValue<string>(connectionName);
            _performanceMeasurementService = performanceMeasurementService;
        }

        /// <summary>
        /// Stores one item
        /// </summary>
        /// <param name="item">The item</param>
        public void Store(SnapshotBase item)
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDatabaseWrites()))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.Parameters.AddWithValue("@data", Serialize(item));
                        command.Parameters.AddWithValue("@aggregateId", item.AggregateId);

                        command.CommandText =
                            $"INSERT INTO {_tableName} ([AggregateId],[Data]) VALUES (@aggregateId, @data)";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Stores a list of items
        /// </summary>
        /// <param name="items">The items</param>
        public void Store(IEnumerable<SnapshotBase> items)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (var item in items)
                {
                    Store(item); //TODO: Use multiple inserts
                }

                transaction.Complete();
            }
        }

        /// <summary>
        /// Selects all items
        /// </summary>
        /// <returns>All items</returns>
        public IEnumerable<SnapshotBase> Select()
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDatabaseReads()))
            {
                var result = new List<SnapshotBase>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT [Data] FROM {_tableName}";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(Deserialize<SnapshotBase>(reader.GetString(0)));
                            }
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Selects all items with the given aggregateId
        /// </summary>
        /// <returns>All items</returns>
        public IEnumerable<SnapshotBase> Select(Guid aggregateId)
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDatabaseReads()))
            {
                var result = new List<SnapshotBase>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.Parameters.AddWithValue("@aggregateId", aggregateId);

                        command.CommandText = $"SELECT [Data] FROM {_tableName} WHERE [AggregateId] = @aggregateId";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(Deserialize<SnapshotBase>(reader.GetString(0)));
                            }
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Delete all documents
        /// </summary>
        public void DeleteAll()
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountDatabaseWrites()))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"DELETE FROM [dbo].[SnapshotDataStore]";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}