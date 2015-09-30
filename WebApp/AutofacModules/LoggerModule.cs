using Autofac;
using AutofacSerilogIntegration;

namespace WebApp.AutofacModules
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterLogger();
        }
    }
}