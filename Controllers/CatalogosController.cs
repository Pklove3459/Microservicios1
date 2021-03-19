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
    public class CatalogosController : ControllerBase
    {
        inventarioContext dbContext;

        private readonly ILogger<CatalogosController> _logger;

        public CatalogosController(ILogger<CatalogosController> logger)
        {
            _logger = logger;
            dbContext = new inventarioContext();
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<Categoria>> Get([FromQuery]int categoriaId = -1)
        {
            List<Categoria> Catalogos = null;
            Catalogos = await dbContext.Categorias
                .Where(p => (categoriaId >= 0 && p.Id == categoriaId) || (categoriaId == -1 && p.Id != categoriaId))
                .ToListAsync();
            if(Catalogos == null) return BadRequest();
            else return Ok(Catalogos);
        }
    }
}
