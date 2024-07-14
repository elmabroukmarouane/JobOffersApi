using StackExchange.Redis;

namespace JobsOffer.Api.Business.Services.Interfaces
{
    public interface IRedisService
    {
        #region GETTERS
        Task<string?> Get(string key);
        Task<RedisValue[]> Get(RedisKey[] redisKeys);
        #endregion

        #region SETTERS
        Task<bool> Set(string key, string value);
        Task<bool> Set(KeyValuePair<RedisKey, RedisValue>[] redisPairs);
        #endregion

        #region DELETTERS
        Task<bool> Delete(string key);
        Task<long> Delete(RedisKey[] redisKeys);
        #endregion
    }
}
