using JobsOffer.Api.Business.Redis.Interface;
using JobsOffer.Api.Business.Services.Interfaces;
using StackExchange.Redis;

namespace JobsOffer.Api.Business.Services.Classes
{
    public class RedisService : IRedisService
    {
        #region ATTRIBUTES
        protected readonly ConnectionMultiplexer _connectionMultiplexer;
        #endregion

        #region CONSTRUCTOR
        public RedisService(IRedisConnectionFactory redisConnectionFactory) => _connectionMultiplexer = redisConnectionFactory.GetConnectionMultiplexer() ?? throw new ArgumentException(null, nameof(redisConnectionFactory));
        #endregion

        #region GETTERS
        public async Task<string?> Get(string key) => await _connectionMultiplexer.GetDatabase().StringGetAsync(key);

        public async Task<RedisValue[]> Get(RedisKey[] redisKeys) => await _connectionMultiplexer.GetDatabase().StringGetAsync(redisKeys);
        #endregion

        #region SETTERS
        public async Task<bool> Set(string key, string value) => await _connectionMultiplexer.GetDatabase().StringSetAsync(key, value);

        public async Task<bool> Set(KeyValuePair<RedisKey, RedisValue>[] redisPairs) => await _connectionMultiplexer.GetDatabase().StringSetAsync(redisPairs);
        #endregion

        #region DELETTERS
        public async Task<bool> Delete(string key) => await _connectionMultiplexer.GetDatabase().KeyDeleteAsync(key);

        public async Task<long> Delete(RedisKey[] redisKeys) => await _connectionMultiplexer.GetDatabase().KeyDeleteAsync(redisKeys);
        #endregion
    }
}
