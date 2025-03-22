var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("pubsub");
var worker = builder.AddDockerfile("worker", "..\\roscoe-capital-worker");

var frontend = builder.AddProject<Projects.roscoe_capital_aspire_Web>("frontend")
    .WaitFor(redis)
    .WaitFor(worker);
builder.Build().Run();
