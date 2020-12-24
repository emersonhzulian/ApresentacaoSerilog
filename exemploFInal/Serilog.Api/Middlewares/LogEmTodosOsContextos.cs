using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Serilog.Api.Middlewares
{
    public class LogEmTodosOsContextos
    {
        private readonly RequestDelegate next;

        public LogEmTodosOsContextos(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            //LogContext.PushProperty("UserName", context.User.Identity.Name);

            LogContext.PushProperty("Headers Enviados Logando em todos os contextos", context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), true);

            return next(context);
        }
    }
}
