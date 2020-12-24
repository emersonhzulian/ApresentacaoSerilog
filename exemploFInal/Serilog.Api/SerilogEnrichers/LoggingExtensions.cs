using System;
using Serilog.Configuration;

namespace Serilog.Api.SerilogEnrichers
{
    public static class LoggingExtensions
    {
        public static LoggerConfiguration WithReleaseNumber(
            this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
                throw new ArgumentNullException(nameof(enrich));

            return enrich.With<ReleaseNumberEnricher>();
        }
    }
}
