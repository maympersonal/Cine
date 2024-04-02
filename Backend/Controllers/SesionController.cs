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
using System.Data;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private readonly ServiceSesion _servicesesion;
        private readonly ServicePelicula _servicepelicula;
        private readonly ServiceSala _servicesala;

        public SesionController (ServiceSesion servicesesion,ServicePelicula servicepelicula,ServiceSala servicesala)
        {
            _servicesesion = servicesesion;
            _servicepelicula=servicepelicula;
            _servicesala = servicesala;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Sesion>>> GetSesions()
        {
            return Ok(await _servicesesion.GetSesions());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<IEnumerable<Sesion>>> GetSesion(int id)
        {
            var pelicula = await _servicepelicula.GetPelicula(id);
            
            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula.Sesions.ToList();
        }

        [HttpPut("Update/{id}")] //no funcional hasta agregaer id a sesion o modificar la capa de servicio de sesion para q busque sesiones mediante idp,ids y fecha
        public async Task<IActionResult> PutSesion(int id, Sesion sesion)
        {
            var Oldsesion = await _servicesesion.GetSesion(id);
            if (id != sesion.IdP)
            {
                return BadRequest();
            }

            try
            {
                await _servicesesion.PutSesion(sesion);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SesionExists(id))
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
        public async Task<ActionResult<SesionDtoIn>> PostSesion(SesionDtoIn sesion)
        {
            var pelicula=await _servicepelicula.GetPelicula(sesion.IdP);
            if(pelicula is null || pelicula.Duración is null) return BadRequest();
            if(await _servicesesion.ExistSesion(sesion.Fecha, (int)pelicula.Duración ,sesion.IdS)) return Conflict();

            var sala = await _servicesala.GetSala(sesion.IdS);
            if(sala is null) return BadRequest();

            var Newsesion = new Sesion
            {
                IdP=sesion.IdP,
                IdS=sesion.IdS,
                Fecha=sesion.Fecha,
                IdSNavigation=sala,
                IdPNavigation=pelicula
            };
            try
            {
                await _servicesesion.PostSesion(Newsesion);
            }
            catch (DbUpdateException)
            {
                if (SesionExists(sesion.IdP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSesion", new { id = Newsesion.IdP }, Newsesion);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _servicesesion.GetSesion(id);
            if (sesion == null)
            {
                return NotFound();
            }

            await _servicesesion.DeleteSesion(id);
            return NoContent();
        }

        private bool SesionExists(int id)
        {
            return _servicesesion.GetSesion(id)!=null;
        }
    }   
}