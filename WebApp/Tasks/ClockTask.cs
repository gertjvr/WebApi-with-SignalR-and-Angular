using System;
using FluentScheduler;
using WebApp.Hubs;
using WebApp.Infrastructure.SignalR;

namespace WebApp.Tasks
{
    public class ClockTask : ITask
    {
        private readonly HubContext<AppHub> _hubContext;

        public ClockTask(HubContext<AppHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void Execute()
        {
            _hubContext.Clients.All.tick(DateTime.UtcNow);
        }
    }
}