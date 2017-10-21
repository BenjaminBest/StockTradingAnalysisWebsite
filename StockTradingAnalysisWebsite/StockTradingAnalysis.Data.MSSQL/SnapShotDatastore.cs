using Newtonsoft.Json;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.DomainContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

namespace StockTradingAnalysis.Data.MSSQL
{
    /// <summary>
    /// Datastore the entry class for the raven db database
    /// </summary>
    public class SnapShotDatastore : ISnapshotDatastore
    {
        private readonly string _tableName;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="connectionName">Name of the connection string</param>
        /// <param name="tableName">Name of the table</param>
        public SnapShotDatastore(string connectionName, string tableName)
        {
            _tableName = tableName;
            _connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// Stores one item
        /// </summary>
        /// <param name="item">The item</param>
        public void Store(SnapshotBase item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@data", Serialize(item));
                    command.Parameters.AddWithValue("@aggregateId", item.AggregateId);

                    command.CommandText = $"INSERT INTO {_tableName} ([AggregateId],[Data]) VALUES (@aggregateId, @data)";
                    command.ExecuteNonQuery();
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

        /// <summary>
        /// Selects all items with the given aggregateId
        /// </summary>
        /// <returns>All items</returns>
        public IEnumerable<SnapshotBase> Select(Guid aggregateId)
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

        /// <summary>
        /// Delete all documents
        /// </summary>
        public void DeleteAll()
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

        /// <summary>
        /// Deserializes the given <param name="value">value</param> into the type T.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="value">Value</param>
        /// <returns></returns>
        private static T Deserialize<T>(string value)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            return string.IsNullOrEmpty(value) ? default(T) : (T)JsonConvert.DeserializeObject(value, settings);
        }

        /// <summary>
        /// Serializes the given <param name="value">value</param> into a string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        private static string Serialize(object value)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            return value == null ? string.Empty : JsonConvert.SerializeObject(value, settings);
        }
    }
}