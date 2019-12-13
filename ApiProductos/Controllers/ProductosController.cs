using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProductos.Data;
using ApiProductos.Models;

namespace ApiProductos.Controllers
{
    [Route("api/Productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public IEnumerable<ProductosModel> GetProductos()
        {
            return _context.Productos;
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductosModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productosModel = await _context.Productos.FindAsync(id);

            if (productosModel == null)
            {
                return NotFound();
            }

            return Ok(productosModel);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductosModel([FromRoute] int id, [FromBody] ProductosModel productosModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productosModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(productosModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<IActionResult> PostProductosModel([FromBody] ProductosModel productosModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Productos.Add(productosModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductosModel", new { id = productosModel.ID }, productosModel);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductosModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productosModel = await _context.Productos.FindAsync(id);
            if (productosModel == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productosModel);
            await _context.SaveChangesAsync();

            return Ok(productosModel);
        }

        private bool ProductosModelExists(int id)
        {
            return _context.Productos.Any(e => e.ID == id);
        }
    }
}