using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.ServiceLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicePago : ControllerBase
    {
        private readonly CineContext _context;

        public ServicePago(CineContext context)
        {
            _context = context;
        }

        // GET: api/ServicePago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        // GET: api/ServicePago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // PUT: api/ServicePago/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.IdPg)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/ServicePago
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPago", new { id = pago.IdPg }, pago);
        }

        // DELETE: api/ServicePago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IdPg == id);
        }
    }
}
