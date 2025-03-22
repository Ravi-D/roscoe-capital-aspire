using Microsoft.AspNetCore.Components;

namespace roscoe_capital_aspire.Web
{
    public interface IRedisPublisher
    {
        public Task<string> PublishToSubscribers(ChangeEventArgs ev);
    }
}