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
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public IEnumerable<CategoriasModel> GetCategorias()
        {
            return _context.Categorias;
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriasModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoriasModel = await _context.Categorias.FindAsync(id);

            if (categoriasModel == null)
            {
                return NotFound();
            }

            return Ok(categoriasModel);
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriasModel([FromRoute] int id, [FromBody] CategoriasModel categoriasModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriasModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(categoriasModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriasModelExists(id))
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

        // POST: api/Categorias
        [HttpPost]
        public async Task<IActionResult> PostCategoriasModel([FromBody] CategoriasModel categoriasModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categorias.Add(categoriasModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriasModel", new { id = categoriasModel.ID }, categoriasModel);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriasModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoriasModel = await _context.Categorias.FindAsync(id);
            if (categoriasModel == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoriasModel);
            await _context.SaveChangesAsync();

            return Ok(categoriasModel);
        }

        private bool CategoriasModelExists(int id)
        {
            return _context.Categorias.Any(e => e.ID == id);
        }
    }
}