using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.ServiceLayer;
using Backend.Models;

namespace Backend.ServiceLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly ServiceActor _service;

        public ActorController (ServiceActor service)
        {
            _service = service;
        }

        // GET: api/ServiceActor
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            return Ok(await _service.GetActors());
        }

        // GET: api/ServiceActor/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            var actor = await _service.GetActor(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/ServiceActor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.IdA)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutActor(id,actor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/ServiceActor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            await _service.PostActor(actor);
            return CreatedAtAction("GetActor", new { id = actor.IdA }, actor);
        }

        // DELETE: api/ServiceActor/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = GetActor(id);
            if (actor == null)
            {
                return NotFound();
            }
            await _service.DeleteActor(id);
            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return _service.GetActor(id) != null;
        }
    }
}
