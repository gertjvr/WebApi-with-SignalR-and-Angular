using System.Collections.Generic;
using FluentScheduler;

namespace WebApp.Infrastructure.FluentScheduler
{
    public class RegistryProvider : Registry
    {
        public RegistryProvider(IEnumerable<IRegistry> registries)
        {
            foreach (var registry in registries)
            {
                registry.AddSchedule(this);
            }
        }
    }
}