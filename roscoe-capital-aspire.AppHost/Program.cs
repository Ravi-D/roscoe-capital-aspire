var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("pubsub", port: 6379).WithRedisCommander();
var worker = builder.AddDockerfile("worker", "..\\roscoe-capital-worker");

var frontend = builder.AddProject<Projects.roscoe_capital_aspire_Web>("frontend")
    .WithEnvironment("REDIS_HOST", "pubsub")
    .WaitFor(redis)
    .WaitFor(worker);

builder.Build().Run();
