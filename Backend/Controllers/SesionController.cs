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
    public class SesionController : ControllerBase
    {
        private readonly ServiceSesion _service;

        public SesionController (ServiceSesion service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Sesion>>> GetSesions()
        {
            return Ok(await _service.GetSesions());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Sesion>> GetSesion(int id)
        {
            var sesion = await _service.GetSesion(id);

            if (sesion == null)
            {
                return NotFound();
            }

            return sesion;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutSesion(int id, Sesion sesion)
        {
            if (id != sesion.IdP)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutSesion(sesion);
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
        public async Task<ActionResult<Sesion>> PostSesion(Sesion sesion)
        {
            try
            {
                await _service.PostSesion(sesion);
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

            return CreatedAtAction("GetSesion", new { id = sesion.IdP }, sesion);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSesion(int id)
        {
            var sesion = await _service.GetSesion(id);
            if (sesion == null)
            {
                return NotFound();
            }

            await _service.DeleteSesion(id);
            return NoContent();
        }

        private bool SesionExists(int id)
        {
            return _service.GetSesion(id)!=null;
        }
    }   
}