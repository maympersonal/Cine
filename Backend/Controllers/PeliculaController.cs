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
    public class PeliculaController : ControllerBase
    {
        private readonly ServicePelicula _service;

        public PeliculaController(ServicePelicula service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPeliculas()
        {
            return Ok(await _service.GetPeliculas());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Pelicula>> GetPelicula(int id)
        {
            var pelicula = await _service.GetPelicula(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutPelicula(int id, Pelicula pelicula)
        {
            if (id != pelicula.IdP)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutPelicula(pelicula);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
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
        public async Task<ActionResult<Pelicula>> PostPelicula(Pelicula pelicula)
        {
            await _service.PostPelicula(pelicula);

            return CreatedAtAction("GetPelicula", new { id = pelicula.IdP }, pelicula);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var pelicula = await _service.GetPelicula(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            await _service.DeletePelicula(id);
            return NoContent();
        }

        private bool PeliculaExists(int id)
        {
            return _service.GetPelicula(id)!=null;
        }
    }   
}