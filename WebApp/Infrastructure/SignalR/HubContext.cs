﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebApp.Infrastructure.SignalR
{
    public class HubContext<T> : IHubContext<T>
        where T : IHub
    {
        readonly IHubContext context;

        public HubContext()
        {
            context = GlobalHost.ConnectionManager.GetHubContext<T>();
        }

        public IHubConnectionContext<dynamic> Clients
        {
            get { return context.Clients; }
        }

        public IGroupManager Groups
        {
            get { return context.Groups; }
        }
    }
}