using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Cliente>> Search()
        {
            return Ok();
        }
    }
}
