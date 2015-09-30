using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Serilog;

namespace WebApp.Hubs
{
    public class AppHub : Hub
    {
        private readonly ILogger _logger;

        public AppHub(ILogger logger)
        {
            _logger = logger;
        }

        public override Task OnConnected()
        {
            _logger.Debug("SignalR client {UserId} {SignalRAction} with connection ID {ConnectionId}", Context.User?.Identity.Name, "connected", Context.ConnectionId);
            Clients.Caller.welcome("Hello");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _logger.Debug("SignalR client {UserId} {SignalRAction} with connection ID {ConnectionId}", Context.User?.Identity.Name, "disconnected", Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            _logger.Debug("SignalR client {UserId} {SignalRAction} with connection ID {ConnectionId}", Context.User?.Identity.Name, "reconnected", Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}