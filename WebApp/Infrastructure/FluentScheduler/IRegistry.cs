using FluentScheduler;

namespace WebApp.Infrastructure.FluentScheduler
{
    public interface IRegistry
    {
        void AddSchedule(Registry registry);
    }
}