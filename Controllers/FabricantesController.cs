using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSInventario.Models;

namespace MSInventario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FabricantesController : ControllerBase
    {
        inventarioContext dbContext;

        private readonly ILogger<FabricantesController> _logger;

        public FabricantesController(ILogger<FabricantesController> logger)
        {
            _logger = logger;
            dbContext = new inventarioContext();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Fabricante>> Get([FromQuery]string nombre = "", [FromQuery]int fabricanteId = -1)
        {
            List<Fabricante> Fabricantes = null;

            Fabricantes = await dbContext.Fabricantes
                .Where(p => (fabricanteId >= 0 && p.Id == fabricanteId) || 
                    (fabricanteId < 0 && p.Id != fabricanteId))
                .Where(p => p.Nombre.Contains(nombre.Trim()))
                .ToListAsync();
            if(Fabricantes == null) return BadRequest();
            else return Ok(Fabricantes);
        }

        [HttpPost("nuevo")]
        public async Task<ActionResult<Fabricante>> Add([FromBody]Fabricante Fabricante)
        {
            if(Fabricante == null)
            {
                _logger.LogError("Body es null");
                return BadRequest("Body es null");
            }

            try
            {
                dbContext.Entry(Fabricante).State = EntityState.Added;
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Se agregó nuevo Fabricante: {0}", Fabricante.Nombre);
                return Created("", Fabricante);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió una excepción:\n" + ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<Fabricante>> Update(int id, [FromBody]Fabricante Fabricante)
        {
            if(Fabricante == null)
            {
                _logger.LogError("Body es null");
                return BadRequest("Body es null");
            }

            try
            {
                dbContext.Entry(Fabricante).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Se actualizó fabricante: {0}", Fabricante.Nombre);
                return Ok(Fabricante);
            }
            catch(DbUpdateConcurrencyException dbex)
            {
                var item = await dbContext.Fabricantes.FirstOrDefaultAsync(p => p.Id == id);
                if(item == null)
                {
                    _logger.LogDebug("No se encontró un fabricante con ID {0}", id.ToString());
                    _logger.LogError("Ocurrió una excepción:\n" + dbex.Message);
                    return NotFound();
                } else return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió una excepción:\n" + ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
