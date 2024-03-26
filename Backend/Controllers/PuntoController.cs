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
    public class PuntoController : ControllerBase
    {
        private readonly ServicePunto _service;

        public PuntoController (ServicePunto service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Punto>>> GetPuntos()
        {
            return Ok(await _service.GetPuntos());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Punto>> GetPunto(int id)
        {
            var punto = await _service.GetPunto(id);

            if (punto == null)
            {
                return NotFound();
            }

            return punto;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutPunto(int id, Punto punto)
        {
            if (id != punto.IdPg)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutPunto(punto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntoExists(id))
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
        public async Task<ActionResult<Punto>> PostPunto(Punto punto)
        {
            try
            {
                await _service.PostPunto(punto);
            }
            catch (DbUpdateException)
            {
                if (PuntoExists(punto.IdPg))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPunto", new { id = punto.IdPg }, punto);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePunto(int id)
        {
            var punto = await _service.GetPunto(id);
            if (punto == null)
            {
                return NotFound();
            }

            await _service.DeletePunto(id);
            return NoContent();
        }

        private bool PuntoExists(int id)
        {
            return _service.GetPunto(id)!=null;
        }
    }   
}