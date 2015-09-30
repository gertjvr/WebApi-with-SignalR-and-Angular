using System;
using System.Reflection;
using Microsoft.AspNet.SignalR.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace WebApp.Infrastructure.SignalR
{
    public class SignalRContractResolver : IContractResolver
    {
        private readonly Assembly _signalRAssembly;
        private readonly IContractResolver _camelCaseContractResolver;
        private readonly IContractResolver _defaultContractSerializer;

        public SignalRContractResolver()
        {
            _defaultContractSerializer = new DefaultContractResolver();
            _camelCaseContractResolver = new CamelCasePropertyNamesContractResolver();
            _signalRAssembly = typeof(Connection).Assembly;
        }

        public JsonContract ResolveContract(Type type)
        {
            if (type.Assembly.Equals(_signalRAssembly))
            {
                var contract = _defaultContractSerializer.ResolveContract(type);
                return contract;
            }
            else
            {
                var contract = _camelCaseContractResolver.ResolveContract(type);
                return contract;
            }
        }
    }
}