using Newtonsoft.Json;
using WebApp.Infrastructure.SignalR;

namespace WebApp.Infrastructure.JsonSerializer
{
    public class CustomJsonSerializer : Newtonsoft.Json.JsonSerializer
    {
        public CustomJsonSerializer()
        {
            TypeNameHandling = TypeNameHandling.Auto;
            ContractResolver = new SignalRContractResolver();
            Formatting = Formatting.None;
            DefaultValueHandling = DefaultValueHandling.Ignore;
            DateParseHandling = DateParseHandling.DateTimeOffset;
            DateFormatHandling = DateFormatHandling.IsoDateFormat;
        }
    }
}