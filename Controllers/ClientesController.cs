
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSClientes.Models;

namespace MSClientes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        clientesContext dbContext;
        private readonly ILogger<ClientesController> log;

        [HttpGet("buscar")]
        public async Task<ActionResult<Cliente>> Search()
        {
            return Ok();
        }

         [HttpPost("RegistrarCliente")]
        public async Task<ActionResult<Cliente>> Add([FromBody]Cliente cliente)
        {
            if(cliente == null)
            {
                log.LogError("Ingreso datos vacios");
                return BadRequest("Ingreso datos vacios");
            }

            try
            {
                dbContext.Entry(cliente).State = EntityState.Added;
                await dbContext.SaveChangesAsync();

                log.LogInformation("Se agregó nuevo cliente: {0}", cliente.Nombre);
                return Created("", cliente);
            }
            catch (Exception ex)
            {
                log.LogError("Sucedio una excepcion:\n" + ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
