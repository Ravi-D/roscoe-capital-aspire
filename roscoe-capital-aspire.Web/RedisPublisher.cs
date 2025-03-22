using Microsoft.AspNetCore.Components;
using StackExchange.Redis;

namespace roscoe_capital_aspire.Web
{
    public class RedisPublisher : IRedisPublisher
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly ILogger<RedisPublisher> _logger;
        
        public RedisPublisher(IConnectionMultiplexer redis, ILogger<RedisPublisher> logger)
        {
            _redis = redis;
            _logger = logger;            
        }
        public ISubscriber GetSubscriber() 
        {
            return _redis.GetSubscriber();
        }

        public async Task<string> TestRedisPublisher(ChangeEventArgs ev)
        {
            _redis.IsConnected.ToString();
            _logger.LogInformation("TestRedisPublisher called with value: {Value}", ev.Value);
            var subscriber = GetSubscriber();
            RedisChannel channel = new RedisChannel("test-channel", RedisChannel.PatternMode.Auto);
            RedisValue message = new RedisValue(ev.Value.ToString());           
            try
            {
                await subscriber.PublishAsync(channel, message);
                _logger.LogInformation("Published message: {Message} to channel: {Channel}", message, channel);
            }
            catch (RedisConnectionException rcex)
            {
                _logger.LogError(rcex, "Error publishing message to Redis");
            }
            return ev.Value.ToString();
        }
    }
}