using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.ServiceLayer;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePunto : ControllerBase
    {
        private readonly CineContext _context;

        public ServicePunto(CineContext context)
        {
            _context = context;
        }

        // GET: api/ServicePunto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Punto>>> GetPuntos()
        {
            return await _context.Puntos.ToListAsync();
        }

        // GET: api/ServicePunto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Punto>> GetPunto(int id)
        {
            var punto = await _context.Puntos.FindAsync(id);

            if (punto == null)
            {
                return NotFound();
            }

            return punto;
        }

        // PUT: api/ServicePunto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPunto(int id, Punto punto)
        {
            if (id != punto.IdPg)
            {
                return BadRequest();
            }

            _context.Entry(punto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntoExists(id))
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

        // POST: api/ServicePunto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Punto>> PostPunto(Punto punto)
        {
            _context.Puntos.Add(punto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PuntoExists(punto.IdPg))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPunto", new { id = punto.IdPg }, punto);
        }

        // DELETE: api/ServicePunto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePunto(int id)
        {
            var punto = await _context.Puntos.FindAsync(id);
            if (punto == null)
            {
                return NotFound();
            }

            _context.Puntos.Remove(punto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuntoExists(int id)
        {
            return _context.Puntos.Any(e => e.IdPg == id);
        }
    }   
}