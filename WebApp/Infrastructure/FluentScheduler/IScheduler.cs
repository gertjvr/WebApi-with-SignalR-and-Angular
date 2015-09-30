namespace WebApp.Infrastructure.FluentScheduler
{
    public interface IScheduler
    {
        void Start();
        void Stop();
    }
}