using StackExchange.Redis;
using System.Runtime.CompilerServices;
using static StackExchange.Redis.RedisChannel;

namespace roscoe_capital_worker
{
    public class ExcelWorker : BackgroundService
    {
        private readonly ILogger<ExcelWorker> _logger;
        private readonly IConnectionMultiplexer _redis;
        private readonly RedisChannel _channel = "excel-channel";

        public ExcelWorker(ILogger<ExcelWorker> logger, IExcelHandler excelhandler, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                ISubscriber subscriber = _redis.GetSubscriber();
                await subscriber.SubscribeAsync(_channel, (channel, message) =>
                {
                    _logger.LogInformation("Received message: {Message}", message);
                    _redis.GetDatabase().StringSet("excel-channel", message);
                });
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
            }
        }
    }
}
