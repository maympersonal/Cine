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
    public class ServiceEfectivo : ControllerBase
    {
        private readonly CineContext _context;

        public ServiceEfectivo(CineContext context)
        {
            _context = context;
        }

        // GET: api/ServiceEfectivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Efectivo>>> GetEfectivos()
        {
            return await _context.Efectivos.ToListAsync();
        }

        // GET: api/ServiceEfectivo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Efectivo>> GetEfectivo(int id)
        {
            var efectivo = await _context.Efectivos.FindAsync(id);

            if (efectivo == null)
            {
                return NotFound();
            }

            return efectivo;
        }

        // PUT: api/ServiceEfectivo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEfectivo(int id, Efectivo efectivo)
        {
            if (id != efectivo.IdPg)
            {
                return BadRequest();
            }

            _context.Entry(efectivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EfectivoExists(id))
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

        // POST: api/ServiceEfectivo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Efectivo>> PostEfectivo(Efectivo efectivo)
        {
            _context.Efectivos.Add(efectivo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EfectivoExists(efectivo.IdPg))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEfectivo", new { id = efectivo.IdPg }, efectivo);
        }

        // DELETE: api/ServiceEfectivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEfectivo(int id)
        {
            var efectivo = await _context.Efectivos.FindAsync(id);
            if (efectivo == null)
            {
                return NotFound();
            }

            _context.Efectivos.Remove(efectivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EfectivoExists(int id)
        {
            return _context.Efectivos.Any(e => e.IdPg == id);
        }
    }   
}