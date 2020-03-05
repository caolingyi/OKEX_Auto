using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace OKEX.Auto.Core.Utilities
{
    public class IdBuilder
    {
        private readonly IDatabase _database;
        public IdBuilder(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase(0);
        }

        public IdBuilder(string connection)
        {
            _database = ConnectionMultiplexer.Connect(connection).GetDatabase(0);
        }

        public async Task<long> NewIdAsync()
        {
            var prexId = (DateTime.Now - DateTime.Parse("2017-12-31")).Ticks.ToString().Substring(0, 6) + DateTime.Now.ToString("HHmm");
            var existed = await _database.KeyExistsAsync(prexId);
            if (!existed)
            {
                await _database.StringSetAsync(prexId, 1, DateTime.Now.AddSeconds(61) - DateTime.Now);
            }
            var incNum = await _database.StringIncrementAsync(prexId);
            return long.Parse(prexId + "00000") + incNum;
        }
    }

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddIdBuilder(this IServiceCollection services, string connectionString = "127.0.0.1:6379")
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            return services.AddSingleton(new IdBuilder(ConnectionMultiplexer.Connect(connectionString)));
        }
    }
    
}
