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
    public class ServiceTarjetum : ControllerBase
    {
        private readonly CineContext _context;

        public ServiceTarjetum(CineContext context)
        {
            _context = context;
        }

        // GET: api/ServiceTarjetum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarjetum>>> GetTarjeta()
        {
            return await _context.Tarjeta.ToListAsync();
        }

        // GET: api/ServiceTarjetum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjetum>> GetTarjetum(string id)
        {
            var tarjetum = await _context.Tarjeta.FindAsync(id);

            if (tarjetum == null)
            {
                return NotFound();
            }

            return tarjetum;
        }

        // PUT: api/ServiceTarjetum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarjetum(string id, Tarjetum tarjetum)
        {
            if (id != tarjetum.CodigoT)
            {
                return BadRequest();
            }

            _context.Entry(tarjetum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetumExists(id))
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

        // POST: api/ServiceTarjetum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tarjetum>> PostTarjetum(Tarjetum tarjetum)
        {
            _context.Tarjeta.Add(tarjetum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TarjetumExists(tarjetum.CodigoT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTarjetum", new { id = tarjetum.CodigoT }, tarjetum);
        }

        // DELETE: api/ServiceTarjetum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarjetum(string id)
        {
            var tarjetum = await _context.Tarjeta.FindAsync(id);
            if (tarjetum == null)
            {
                return NotFound();
            }

            _context.Tarjeta.Remove(tarjetum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarjetumExists(string id)
        {
            return _context.Tarjeta.Any(e => e.CodigoT == id);
        }
    }   
}