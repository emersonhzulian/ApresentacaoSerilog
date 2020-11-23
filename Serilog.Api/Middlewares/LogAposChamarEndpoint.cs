using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Events;

namespace Serilog.Api.Middlewares
{
    class LogAposChamarEndpoint
    {
        const string MessageTemplate = "Teste bom: HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        static readonly ILogger Log = Serilog.Log.ForContext<LogAposChamarEndpoint>();

        readonly RequestDelegate _next;

        public LogAposChamarEndpoint(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var sw = Stopwatch.StartNew();

            //Vai chamar nosso endpoint
            await _next(httpContext);

            sw.Stop();

            var statusCode = httpContext.Response?.StatusCode;
            
            var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

            var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;
            
            log.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
        }

        static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = Log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }
    }
}
