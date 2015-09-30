using System;
using System.Reflection;
using Serilog;
using Serilog.Events;
using SerilogWeb.Classic.Enrichers;

namespace WebApp.Infrastructure.Logger
{
    public class LoggerFactory
    {
        private readonly LogEventLevel _minimumLogLevel;
        private readonly string _seqServerUri;
        private readonly Assembly _thisAssembly = typeof (LoggerFactory).Assembly;

        public LoggerFactory(LogEventLevel minimumLogLevel, string seqServerUri)
        {
            _minimumLogLevel = minimumLogLevel;
            _seqServerUri = seqServerUri;
        }
        
        public ILogger CreateLogger()
        {
            var config = GetConfiguration();

            return config.CreateLogger();
        }

        public virtual LoggerConfiguration GetConfiguration()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Is(_minimumLogLevel)
                .WriteTo.ColoredConsole()
                .WriteTo.Seq(_seqServerUri.ToString())
                .WriteTo.Trace()

                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithProperty("ApplicationName", _thisAssembly.GetName().Name)
                .Enrich.WithProperty("ApplicationVersion", _thisAssembly.GetName().Version)
                .Enrich.WithProperty("UserName", Environment.UserName)
                .Enrich.WithProperty("AppDomain", AppDomain.CurrentDomain)
                .Enrich.WithProperty("RuntimeVersion", Environment.Version)
                .Enrich.WithProperty("OSVersion", Environment.OSVersion)
                .Enrich.With<HttpRequestClientHostIPEnricher>()
                .Enrich.With<HttpRequestClientHostNameEnricher>()
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<HttpRequestNumberEnricher>()
                .Enrich.With<HttpRequestRawUrlEnricher>()
                .Enrich.With<HttpRequestTraceIdEnricher>()
                .Enrich.With<HttpRequestTypeEnricher>()
                .Enrich.With<HttpRequestUrlReferrerEnricher>()
                .Enrich.With<HttpRequestUserAgentEnricher>()
                .Enrich.With<UserNameEnricher>()
                // this ensures that calls to LogContext.PushProperty will cause the logger to be enriched
                .Enrich.FromLogContext();
        }
    }    
}