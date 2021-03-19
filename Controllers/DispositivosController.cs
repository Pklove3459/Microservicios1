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
    public class DispositivosController : ControllerBase
    {
        inventarioContext dbContext;

        private readonly ILogger<DispositivosController> _logger;

        public DispositivosController(ILogger<DispositivosController> logger)
        {
            _logger = logger;
            dbContext = new inventarioContext();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Dispositivo>> Get([FromQuery]string nombre = "", [FromQuery]int fabricanteId = -1)
        {
            List<Dispositivo> dispositivos = null;

            dispositivos = await dbContext.Dispositivos
                .Where(p => p.Nombre.Contains(nombre.Trim()))
                .Where(p => (fabricanteId >= 0 && p.Fabricante == fabricanteId) || 
                    (fabricanteId < 0 && p.Fabricante != fabricanteId))
                .ToListAsync();
            if(dispositivos == null) return BadRequest();
            else return Ok(dispositivos);
        }

        [HttpPost("nuevo")]
        public async Task<ActionResult<Dispositivo>> Add([FromBody]Dispositivo dispositivo)
        {
            if(dispositivo == null)
            {
                _logger.LogError("Body es null");
                return BadRequest("Body es null");
            }

            try
            {
                dbContext.Entry(dispositivo).State = EntityState.Added;
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Se agregó nuevo dispositivo: {0}", dispositivo.Nombre);
                return Created("", dispositivo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió una excepción:\n" + ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<Dispositivo>> Update(int id, [FromBody]Dispositivo dispositivo)
        {
            if(dispositivo == null)
            {
                _logger.LogError("Body es null");
                return BadRequest("Body es null");
            }

            try
            {
                dbContext.Entry(dispositivo).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Se actualizó dispositivo: {0}", dispositivo.Nombre);
                return Ok(dispositivo);
            }
            catch(DbUpdateConcurrencyException dbex)
            {
                var item = await dbContext.Dispositivos.FirstOrDefaultAsync(p => p.Id == id);
                if(item == null)
                {
                    _logger.LogDebug("No se encontró un dispositivo con ID {0}", id.ToString());
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
