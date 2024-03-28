using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.ServiceLayer;
using Backend.Models;
using Backend.Data.DTOs;

namespace Backend.Controllers
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


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            return Ok(await _service.GetActors());
        }


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


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutActor(int id, ActorDtoIn actor)
        {
            var NewActor = await _service.GetActor(id);
            if (id != actor.IdA)
            {
                return BadRequest();
            }
            else if( NewActor is not null)
            {
                NewActor.NombreA=actor.NombreA;
            }
            else return BadRequest();

            try
            {
                await _service.PutActor(NewActor);
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


        [HttpPost("Create")]
        public async Task<ActionResult<Actor>> PostActor(ActorDtoIn actor)
        {
            var NewActor = new Actor
            {
                NombreA = actor.NombreA
            };
            await _service.PostActor(NewActor);
            return CreatedAtAction("GetActor", new { id = actor.IdA }, actor);
        }

        //falla
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _service.GetActor(id);
            if (actor == null)
            {
                return NotFound();
            }
            await _service.DeleteActor(id);
            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return  _service.GetActor(id)!=null;
        }
    }
}
