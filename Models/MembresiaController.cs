using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSClientes.Models
{
    [Route("[controller]")]
    [ApiController]
    public class MembresiaController : ControllerBase
    {
        clientesContext bd;


        private readonly ILogger<MembresiaController> _logger;

        public MembresiaController(ILogger<MembresiaController> logger)
        {
            _logger = logger;
            bd = new clientesContext();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Membresia>> Get([FromQuery] int idCliente = -1, [FromQuery] int tipo = -1)
        {
            List<Membresia> membresia = null;

            membresia = await bd.Membresias
                .Where(p => (p.IdCliente >= 0 && p.IdCliente == idCliente))
                .Where(p => (p.Tipo >= 0 && p.Tipo == tipo))
                .ToListAsync();
            if (membresia == null) return BadRequest();
            else return Ok(membresia);
        }
    }
}
