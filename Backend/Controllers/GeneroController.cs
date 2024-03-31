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
    public class GeneroController : ControllerBase
    {
        private readonly ServiceGenero _service;

        public GeneroController (ServiceGenero service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return Ok(await _service.GetGeneros());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Genero>> GetGenero(int id)
        {
            var genero = await _service.GetGenero(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutGenero(int id, Genero genero)
        {
            if (id != genero.IdG)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutGenero(genero);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
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
        public async Task<ActionResult<Genero>> PostGenero(Genero genero)
        {
            await _service.PostGenero(genero);

            return CreatedAtAction("GetGenero", new { id = genero.IdG }, genero);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _service.GetGenero(id);
            if (genero == null)
            {
                return NotFound();
            }

            await _service.DeleteGenero(id);
            return NoContent();
        }

        private bool GeneroExists(int id)
        {
            return _service.GetGenero(id)!=null;
        }
    }    
}