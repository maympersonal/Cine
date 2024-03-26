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
    public class ServiceWeb : ControllerBase
    {
        private readonly CineContext _context;

        public ServiceWeb(CineContext context)
        {
            _context = context;
        }

        // GET: api/ServiceWeb
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Web>>> GetWebs()
        {
            return await _context.Webs.ToListAsync();
        }

        // GET: api/ServiceWeb/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Web>> GetWeb(int id)
        {
            var web = await _context.Webs.FindAsync(id);

            if (web == null)
            {
                return NotFound();
            }

            return web;
        }

        // PUT: api/ServiceWeb/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeb(int id, Web web)
        {
            if (id != web.IdPg)
            {
                return BadRequest();
            }

            _context.Entry(web).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebExists(id))
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

        // POST: api/ServiceWeb
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Web>> PostWeb(Web web)
        {
            _context.Webs.Add(web);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WebExists(web.IdPg))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeb", new { id = web.IdPg }, web);
        }

        // DELETE: api/ServiceWeb/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeb(int id)
        {
            var web = await _context.Webs.FindAsync(id);
            if (web == null)
            {
                return NotFound();
            }

            _context.Webs.Remove(web);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebExists(int id)
        {
            return _context.Webs.Any(e => e.IdPg == id);
        }
    }  
}