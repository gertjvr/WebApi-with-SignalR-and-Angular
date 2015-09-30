using System.Collections.Generic;
using Autofac;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Owin;

namespace WebApp.Infrastructure.SignalR
{
    public static class SignalRExtensions
    {
        public static void UseSignalR(this IAppBuilder app, string path, IContainer container)
        {
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(container);

            var modules = container.Resolve<IEnumerable<HubPipelineModule>>();

            foreach (var module in modules)
            {
                GlobalHost.HubPipeline.AddModule(module);
            }

            app.MapSignalR(path, new HubConfiguration
            {
#if DEBUG
                EnableDetailedErrors = true,
#endif
                EnableJSONP = false,
                EnableJavaScriptProxies = false,
            });

            //GlobalHost.HubPipeline.RequireAuthentication();
        }
    }
}