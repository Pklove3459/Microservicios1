using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSClientes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSClientes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet("buscar")]
        public async Task<ActionResult<Cliente>> Search([FromQuery]string nombre = "", [FromQuery] int idCliente = -1)
        {
            List<Cliente> clientes = null;
            
            clientesContext dbContext = new clientesContext();

            clientes = await dbContext.Clientes
                .Where(cliente => cliente.Nombre.Contains(nombre))
                .Where(cliente => (idCliente >= 0 && cliente.Id == idCliente) || 
                (idCliente < 0 && cliente.Id != idCliente))
                .ToListAsync();

            if (clientes == null)
            {
                return BadRequest();
            }

            return Ok(clientes);
        }
    }
}
