using System.Linq;
using FluentScheduler;
using Serilog;

namespace WebApp.Infrastructure.FluentScheduler
{
    public class Scheduler : IScheduler
    {
        private readonly AutofacTaskFactory _autofacTaskFactory;
        private readonly RegistryProvider _registryProvider;

        public Scheduler(AutofacTaskFactory autofacTaskFactory, RegistryProvider registryProvider)
        {
            _autofacTaskFactory = autofacTaskFactory;
            _registryProvider = registryProvider;
        }

        public void Start()
        {
            TaskManager.TaskEnd += (s, e) => LogNextRunTime();
            TaskManager.TaskFactory = _autofacTaskFactory;
            TaskManager.Initialize(_registryProvider);

            LogNextRunTime();
        }

        private void LogNextRunTime()
        {
            var nextToRun = TaskManager.AllSchedules.OrderBy(x => x.NextRunTime).First();
            Log.Information("Next schedule {Schedule} will run at {NextRunTime}", nextToRun.Name, nextToRun.NextRunTime);
        }

        public void Stop()
        {
            TaskManager.Stop();
        }
    }
}