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

            string redisHost = builder.Configuration["pubsub:service:host"] ?? "pubsub";
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                ConfigurationOptions config = new ConfigurationOptions
                {
                    EndPoints = { $"{redisHost}:6379" },
                    ConnectRetry = 1,
                    ConnectTimeout = 5000,
                    AbortOnConnectFail = false
                };
                return ConnectionMultiplexer.Connect(config);
            });

            builder.Build().Run();
        }
    }
}