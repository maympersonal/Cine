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
    public class ServiceButaca : ControllerBase
    {
        private readonly CineContext _context;

        public ServiceButaca(CineContext context)
        {
            _context = context;
        }

        // GET: api/ServiceButaca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Butaca>>> GetButacas()
        {
            return await _context.Butacas.ToListAsync();
        }

        // GET: api/ServiceButaca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Butaca>> GetButaca(int id)
        {
            var butaca = await _context.Butacas.FindAsync(id);

            if (butaca == null)
            {
                return NotFound();
            }

            return butaca;
        }

        // PUT: api/ServiceButaca/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutButaca(int id, Butaca butaca)
        {
            if (id != butaca.IdB)
            {
                return BadRequest();
            }

            _context.Entry(butaca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ButacaExists(id))
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

        // POST: api/ServiceButaca
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Butaca>> PostButaca(Butaca butaca)
        {
            _context.Butacas.Add(butaca);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetButaca", new { id = butaca.IdB }, butaca);
        }

        // DELETE: api/ServiceButaca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteButaca(int id)
        {
            var butaca = await _context.Butacas.FindAsync(id);
            if (butaca == null)
            {
                return NotFound();
            }

            _context.Butacas.Remove(butaca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ButacaExists(int id)
        {
            return _context.Butacas.Any(e => e.IdB == id);
        }
    }
}
