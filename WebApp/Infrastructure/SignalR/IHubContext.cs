using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApp.Infrastructure.SignalR
{
    public interface IHubContext<T> : IHubContext
        where T : IHub
    {
    }
}