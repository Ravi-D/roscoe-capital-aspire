using StackExchange.Redis;

namespace roscoe_capital_worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<ExcelWorker>();
            builder.Services.AddSingleton<IExcelHandler, ExcelHandler>();
            
            var redisHost = builder.Configuration["REDIS_HOST"] ?? "pubsub";
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var config = new ConfigurationOptions
                {
                    EndPoints = { $"{redisHost}:6379" },
                    ConnectRetry = 1,
                    ConnectTimeout = 5000,
                    AbortOnConnectFail = false
                };
                return ConnectionMultiplexer.Connect(config);
            });

            IHost host = builder.Build();
            host.Run();
        }
    }
}