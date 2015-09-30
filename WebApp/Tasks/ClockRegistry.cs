using FluentScheduler;
using WebApp.Infrastructure.FluentScheduler;

namespace WebApp.Tasks
{
    public class ClockRegistry : IRegistry
    {
        public void AddSchedule(Registry registry)
        {
            registry.Schedule<ClockTask>().ToRunNow()
                .AndEvery(10).Seconds();
        }
    }
}