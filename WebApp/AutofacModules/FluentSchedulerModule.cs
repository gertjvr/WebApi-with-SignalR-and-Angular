using Autofac;
using FluentScheduler;
using WebApp.Infrastructure.FluentScheduler;

namespace WebApp.AutofacModules
{
    public class FluentSchedulerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacTaskFactory>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<RegistryProvider>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .As<IRegistry>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .As<ITask>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<Scheduler>()
                .As<IScheduler>()
                .InstancePerLifetimeScope();
        }
    }
}