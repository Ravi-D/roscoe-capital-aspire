using Microsoft.Extensions.Logging;
using roscoe_capital_aspire.Web;
using roscoe_capital_aspire.Web.Components;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.Services.AddSingleton<IRedisPublisher, RedisPublisher>();

//var redisHost = builder.Configuration["REDIS_HOST"] ?? "pubsub";
var redisHost = builder.Configuration["pubsub:service:host"] ?? "localhost";
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => 
{
    var config = new ConfigurationOptions
    {
        EndPoints = { $"{redisHost}:6379" },
        ConnectRetry = 2,
        ConnectTimeout = 10_000,
        AbortOnConnectFail = false
    };
    return ConnectionMultiplexer.Connect(config);
});

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
//app.UseOutputCache();
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapDefaultEndpoints();

app.Run();
