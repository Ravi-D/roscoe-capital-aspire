IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<RedisResource> redis = builder.AddRedis("pubsub", port: 6379).WithRedisCommander();
IResourceBuilder<ContainerResource> worker = builder.AddDockerfile("worker", "..\\roscoe-capital-worker");

IResourceBuilder<ProjectResource> frontend = builder.AddProject<Projects.roscoe_capital_aspire_Web>("frontend")
    .WithEnvironment("REDIS_HOST", "pubsub")
    .WaitFor(redis)
    .WaitFor(worker);

builder.Build().Run();
