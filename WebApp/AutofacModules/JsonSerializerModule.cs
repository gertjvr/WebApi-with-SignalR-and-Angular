using Autofac;
using Newtonsoft.Json;
using WebApp.Infrastructure.JsonSerializer;

namespace WebApp.AutofacModules
{
    public class JsonSerializerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterInstance<JsonSerializer>(new CustomJsonSerializer())
                .SingleInstance();
        }
    }
}