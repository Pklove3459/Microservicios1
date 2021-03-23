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
    public class MembresiasController : ControllerBase
    {

        clientesContext db;


        public MembresiasController ()
        {

            db = new clientesContext();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Cliente>> Search()
        {
            return Ok();
        }
        [HttpPost("crear")]
        public async Task<ActionResult<Cliente>> Create([FromBody]Membresia membresia)
        {
            if(membresia == null){
                    return BadRequest("membresia nula");
            }

            try{
                
                Console.WriteLine("--------------------POST-----------------------");
                db.Entry(membresia).State = EntityState.Added;
                await db.SaveChangesAsync();
                
                return Created("",membresia);

            }catch(Exception excepcion){

                return BadRequest(excepcion);
            }

        }
    }
}
