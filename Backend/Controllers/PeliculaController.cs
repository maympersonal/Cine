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
    public class PeliculaController : ControllerBase
    {
        private readonly ServicePelicula _servicepelicula;
        private readonly ServiceActor _serviceactor;
        private readonly ServiceGenero _servicegenero;

        public PeliculaController(ServicePelicula servicepelicula,ServiceActor serviceactor,ServiceGenero servicegenero)
        {
            _servicepelicula = servicepelicula;
            _serviceactor=serviceactor;
            _servicegenero=servicegenero;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPeliculas()
        {
            return Ok(await _servicepelicula.GetPeliculas());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Pelicula>> GetPelicula(int id)
        {
            var pelicula = await _servicepelicula.GetPelicula(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutPelicula(int id, PeliculaDtoIn pelicula)
        {
            var Oldpelicula = await _servicepelicula.GetPelicula(id);
            if (Oldpelicula is null)
            {
                return BadRequest();
            }
            ICollection<Actor> actors= new List<Actor>();
            ICollection<Genero> generos=new List<Genero>();

            foreach ( int idA in pelicula.IdAs )
            {
                var actor = await _serviceactor.GetActor(idA);
                if(actor is null) return BadRequest();
                actors.Add(actor);
            }
            foreach ( int idG in pelicula.IdGs )
            {
                var genero = await _servicegenero.GetGenero(idG);
                if(genero is null) return BadRequest();
                generos.Add(genero);
            }

            Oldpelicula.Sinopsis=pelicula.Sinopsis;
            Oldpelicula.Anno=pelicula.Anno;
            Oldpelicula.Nacionalidad=pelicula.Nacionalidad;
            Oldpelicula.Duración=pelicula.Duración;
            Oldpelicula.Titulo=pelicula.Titulo;
            Oldpelicula.Imagen=pelicula.Imagen;
            Oldpelicula.Trailer=pelicula.Trailer;
            Oldpelicula.IdAs=actors;
            Oldpelicula.IdGs=generos;

            try
            {
                await _servicepelicula.PutPelicula(Oldpelicula);
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
        public async Task<ActionResult<Pelicula>> PostPelicula(PeliculaDtoIn pelicula)
        {
            ICollection<Actor> actors= new List<Actor>();
            ICollection<Genero> generos=new List<Genero>();

            foreach ( int idA in pelicula.IdAs )
            {
                var actor = await _serviceactor.GetActor(idA);
                if(actor is null) return BadRequest();
                actors.Add(actor);
            }
            foreach ( int idG in pelicula.IdGs )
            {
                var genero = await _servicegenero.GetGenero(idG);
                if(genero is null) return BadRequest();
                generos.Add(genero);
            }
            var Newpelicula= new Pelicula
            {
                Sinopsis=pelicula.Sinopsis,
                Anno=pelicula.Anno,
                Nacionalidad=pelicula.Nacionalidad,
                Duración=pelicula.Duración,
                Titulo=pelicula.Titulo,
                Imagen=pelicula.Imagen,
                Trailer=pelicula.Trailer,
                IdAs=actors,
                IdGs=generos
            };
            await _servicepelicula.PostPelicula(Newpelicula);

            return CreatedAtAction("GetPelicula", new { id = Newpelicula.IdP }, Newpelicula);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var pelicula = await _servicepelicula.GetPelicula(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            await _servicepelicula.DeletePelicula(id);
            return NoContent();
        }

        private bool PeliculaExists(int id)
        {
            return _servicepelicula.GetPelicula(id)!=null;
        }
    }   
}