using Autofac;
using Microsoft.AspNet.SignalR.Hubs;
using WebApp.Infrastructure.SignalR;

namespace WebApp.AutofacModules
{
    public class SignalRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterHubs(ThisAssembly);

            builder.RegisterGeneric(typeof(HubContext<>))
                .As(typeof(HubContext<>))
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<Broadcaster>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsAssignableTo<HubPipelineModule>())
                .AsSelf()
                .As<HubPipelineModule>()
                .SingleInstance();
        }
    }
}