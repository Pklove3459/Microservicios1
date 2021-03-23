using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GatewayTienda.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MSClientes.Models;
using MSInventario.Models;

namespace GatewayTienda
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly ILogger<ComprasController> _logger;
        private InventarioClient inventarioClient;
        private ClientesClient clientesClient;

        public ComprasController(ILogger<ComprasController> logger)
        {
            _logger = logger;
            inventarioClient = new InventarioClient();
            clientesClient = new ClientesClient();
        }

        [HttpGet("clientes")]
        public async Task<ActionResult<Cliente>> Get([FromQuery]string nombre = "", [FromQuery]int idCliente = -1)
        {
            var value = await clientesClient.BuscaClientes(nombre, idCliente);
            if(value.Count() > 0)
            {
                return Ok(value);
            } else return BadRequest();
        }

        [HttpGet("dispositivo")]
        public async Task<ActionResult<Dispositivo>> GetDispositivo()
        {
            var value = await inventarioClient.BuscaDispositivo();
            if(value.Count() > 0)
            {
                return Ok(value);
            } else return BadRequest();
        }
    }
}