using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Autofac.Builder;
using Serilog;
using WebApp.AutofacModules;

namespace WebApp
{
    public static class IoC
    {
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Io")]
        public static IContainer LetThereBeIoC(ContainerBuildOptions containerBuildOptions = ContainerBuildOptions.None, Action<ContainerBuilder> preHooks = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<FluentSchedulerModule>();
            builder.RegisterModule<JsonSerializerModule>();
            builder.RegisterModule<LoggerModule>();
            builder.RegisterModule<SignalRModule>();

            try
            {
                var sw = Stopwatch.StartNew();
                if (preHooks != null) preHooks(builder);
                var container = builder.Build(containerBuildOptions);
                sw.Stop();
                container.Resolve<ILogger>().Information("Container built in {Elapsed}", sw.Elapsed);
                return container;
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Container failed to build: {Message}", exc.Message);
            }

            return null;
        }
    }
}