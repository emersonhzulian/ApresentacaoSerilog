using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Api.SerilogEnrichers
{
    public class ReleaseNumberEnricher : ILogEventEnricher
    {
        public const string PropertyName = "ReleaseNumber";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var value = Environment.GetEnvironmentVariable("RELEASE_NUMBER") ?? "local";
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PropertyName, value));
        }

        private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
        {
            var value = Environment.GetEnvironmentVariable("RELEASE_NUMBER") ?? "local";
            return propertyFactory.CreateProperty(PropertyName, value);
        }
    }

}
