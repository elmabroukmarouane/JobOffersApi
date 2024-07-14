using StackExchange.Redis;

namespace JobsOffer.Api.Business.Redis.Interface
{
    public interface IRedisConnectionFactory
    {
        ConnectionMultiplexer GetConnectionMultiplexer();
    }
}
