using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;


namespace Serilog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Ainda não temos a injeção de dependência
            
            var appSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            Log.Logger  = new LoggerConfiguration()
                .ReadFrom.Configuration(appSettings)
                .CreateLogger();
            
            try
            {
                //Repare que estamos utilizando o log do Serilog já, na proxima chamada ele já estará dentro da interface ILogger, aidna não temos a injeção de dependência
                Log.Information("Iniciando aplicação.");
                
                CreateHostBuilder(args).Build().Run();
            }
            catch
            {
                Log.Fatal("Deu ruim, aplicação nem subiu.");
            }
            finally
            {
                //Uma formalidade para que ele feche corretamente os logs (arquivos, memória etc) antes de fechar a aplicação.
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
