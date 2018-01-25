using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
using Newtonsoft.Json;
using StockTradingAnalysis.Interfaces.Data;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Data.MSSQL
{
    /// <summary>
    /// Datastore the entry class for the raven db database
    /// </summary>
    public class EventDatastore : IEventDatastore
    {
        private readonly string _tableName;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="connectionName">Name of the connection string</param>
        /// <param name="tableName">Name of the database table</param>
        public EventDatastore(string connectionName, string tableName)
        {
            _tableName = tableName;
            _connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        /// <summary>
        /// Stores one item
        /// </summary>       
        /// <param name="item">The item</param>
        public void Store(IDomainEvent item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@data", Serialize(item));
                    command.Parameters.AddWithValue("@aggregateId", item.AggregateId);
                    command.Parameters.AddWithValue("@version", item.Version);
                    command.Parameters.AddWithValue("@timestamp", item.TimeStamp);

                    command.CommandText = $"INSERT INTO {_tableName} ([AggregateId], [Version], [Data], [TimeStamp]) VALUES (@aggregateId, @version, @data, @timestamp)";
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Stores a list of items
        /// </summary>
        /// <param name="items">The items</param>
        public void Store(IEnumerable<IDomainEvent> items)
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
        public IEnumerable<IDomainEvent> Select()
        {
            var result = new List<IDomainEvent>();

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
                            result.Add(Deserialize<IDomainEvent>(reader.GetString(0)));
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
        public IEnumerable<IDomainEvent> Select(Guid aggregateId)
        {
            var result = new List<IDomainEvent>();

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
                            result.Add(Deserialize<IDomainEvent>(reader.GetString(0)));
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns all events which matches the given <paramref name="aggregateId" /> from the internal storage
        /// </summary>
        /// <param name="aggregateId">The Id of the aggreate to query the document data store</param>
        /// <param name="minVersion">The minimum version of the aggregate</param>
        /// <returns>A list with all events</returns>
        public IEnumerable<IDomainEvent> Select(Guid aggregateId, int minVersion)
        {
            var result = new List<IDomainEvent>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@aggregateId", aggregateId);
                    command.Parameters.AddWithValue("@minVersion", minVersion);

                    command.CommandText = $"SELECT [Data] FROM {_tableName} WHERE [AggregateId] = @aggregateId AND [Version] > @minVersion";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(Deserialize<IDomainEvent>(reader.GetString(0)));
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
                    command.CommandText = $"DELETE FROM [dbo].[EventDataStore]";
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
        internal static T Deserialize<T>(string value)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new JsonPrivateSetterContractResolver()
            };

            return string.IsNullOrEmpty(value) ? default(T) : (T)JsonConvert.DeserializeObject(value, settings);
        }

        /// <summary>
        /// Serializes the given <param name="value">value</param> into a string
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
        internal static string Serialize(object value)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new JsonPrivateSetterContractResolver()
            };

            return value == null ? string.Empty : JsonConvert.SerializeObject(value, settings);
        }
    }
}