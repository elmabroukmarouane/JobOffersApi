using JobsOffer.Api.Business.Redis.Interface;
using StackExchange.Redis;

namespace JobsOffer.Api.Business.Redis.Class
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        #region ATTRIBUTES
        protected readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        #endregion

        #region CONSTRUCTOR
        public RedisConnectionFactory(string connectionString) => _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        #endregion

        #region METHODS
        public ConnectionMultiplexer GetConnectionMultiplexer() => _connectionMultiplexer.Value;
        #endregion
    }
}
