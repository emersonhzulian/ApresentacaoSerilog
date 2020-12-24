using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Serilog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeuController : ControllerBase
    {
        [HttpGet]
        [Route("ExemploSimples")]
        public IActionResult ExemploSimples([FromServices] ILogger<MeuController> _logger)
        {
            _logger.LogInformation("Acessou o nosso endpoint.");
            
            try
            {
                for(int i =0; i< 100; i++)
                {
                    if(i == 20)
                    {
                        throw new Exception("Problema!!!!!");
                    }
                    else
                    {
                        _logger.LogInformation("O valor de i é {ValorDeI}", i);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro no endpoint.");
            }
            return Ok();
        }
    }
}
