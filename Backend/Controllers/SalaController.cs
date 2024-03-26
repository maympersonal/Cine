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
    public class SalaController : ControllerBase
    {
        private readonly ServiceSala _service;

        public SalaController (ServiceSala service)
        {
            _service = service;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            return Ok(await _service.GetSalas());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _service.GetSala(id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutSala(int id, Sala sala)
        {
            if (id != sala.IdS)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutSala(sala);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(id))
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
        public async Task<ActionResult<Sala>> PostSala(Sala sala)
        {
            await _service.PostSala(sala);

            return CreatedAtAction("GetSala", new { id = sala.IdS }, sala);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _service.GetSala(id);
            if (sala == null)
            {
                return NotFound();
            }

            await _service.DeleteSala(id);
            return NoContent();
        }

        private bool SalaExists(int id)
        {
            return _service.GetSala(id)!=null;
        }
    }   
}