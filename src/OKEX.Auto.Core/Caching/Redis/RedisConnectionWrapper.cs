using StackExchange.Redis;
using System;
using System.Net;

namespace OKEX.Auto.Core.Caching.Redis
{
    public class RedisConnectionWrapper
    {
        private readonly string _connectionString;
        private volatile ConnectionMultiplexer _connection;
        private readonly object _lock = new object();
        private readonly int _db;
        public RedisConnectionWrapper(string connectionString, int db = -1)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connectionString = connectionString;
            _db = db;
        }
        public IDatabase GetDatabase()
        {
            return GetConnection().GetDatabase(_db);
        }
        public IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }
        public EndPoint[] GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }
        public void FlushDatabase(int? db = null)
        {
            var endPoints = GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                GetServer(endPoint).FlushDatabase(db ?? -1);
            }
        }
        public void Dispose()
        {
            _connection?.Dispose();
        }
        protected ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;
            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                _connection?.Dispose();

                _connection = ConnectionMultiplexer.Connect(_connectionString);
            }
            return _connection;
        }
    }
}
