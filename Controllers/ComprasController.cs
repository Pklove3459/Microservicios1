using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MSCompras.Models;

namespace MSInventario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComprasController : ControllerBase
    {
        comprasContext dbContext;

        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ILogger<ComprasController> logger)
        {
            _logger = logger;
            dbContext = new comprasContext();
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<Compra>> Get([FromQuery]int CompraId = -1)
        {
            List<Compra> Compras = null;

            Compras = await dbContext.Compras
                .Where(p => (CompraId >= 0 && p.Id == CompraId) || 
                    (CompraId < 0 && p.Id != CompraId))
                .ToListAsync();
            if(Compras == null) return BadRequest();
            else return Ok(Compras);
        }

        [HttpGet("detalles/{id}")]
        public async Task<ActionResult<Compra>> GetDetails(int id)
        {
            Compra compra = await dbContext.Compras
                .Where(p => p.Id == id)
                .Include(r => r.ProductosCompras)
                .FirstOrDefaultAsync();
            if(compra != null)
            {
                foreach (var item in compra.ProductosCompras)
                {
                    item.IdCompraNavigation = null;
                }
                return Ok(compra);
            } else return BadRequest("No existe compra"); // Lo ideal es que regresara un objeto de error
        }

        [HttpPost("nuevo")]
        public async Task<ActionResult<Compra>> Add([FromBody]Compra Compra)
        {
            if(Compra == null)
            {
                _logger.LogError("Body es null");
                return BadRequest("Body es null");
            }

            try
            {
                dbContext.Entry(Compra).State = EntityState.Added;
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Se agregó nuevo Compra: {0}", Compra.Id);
                return Created("", Compra);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió una excepción:\n" + ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("nuevo/detalle/{idCompra}")]
        public async Task<ActionResult<Compra>> AddDetails([FromBody]ProductosCompra[] productos, int idCompra = -1)
        {
            if(idCompra < 0) return BadRequest("ID necesario");
            if(productos == null || productos.Count() == 0) return BadRequest();

            try
            {
                foreach (ProductosCompra producto in productos)
                {
                    dbContext.Entry(producto).State = EntityState.Added;   
                }
                await dbContext.SaveChangesAsync();
                return Created("", productos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ocurrió una excepción:\n" + ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<Compra>> Update(int id, [FromBody]Compra Compra)
        {
            if(Compra == null)
            {
                _logger.LogError("Body es null");
                return BadRequest("Body es null");
            }

            try
            {
                dbContext.Entry(Compra).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                _logger.LogInformation("Se actualizó Compra: {0}", Compra.Id);
                return Ok(Compra);
            }
            catch(DbUpdateConcurrencyException dbex)
            {
                var item = await dbContext.Compras.FirstOrDefaultAsync(p => p.Id == id);
                if(item == null)
                {
                    _logger.LogDebug("No se encontró un Compra con ID {0}", id.ToString());
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
