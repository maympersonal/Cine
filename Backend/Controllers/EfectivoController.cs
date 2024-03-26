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
    public class EfectivoController : ControllerBase
    {
        private readonly ServiceEfectivo _service;

        public EfectivoController(ServiceEfectivo service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Efectivo>>> GetEfectivos()
        {
            return Ok(await _service.GetEfectivos());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Efectivo>> GetEfectivo(int id)
        {
            var efectivo = await _service.GetEfectivo(id);

            if (efectivo == null)
            {
                return NotFound();
            }

            return efectivo;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutEfectivo(int id, Efectivo efectivo)
        {
            if (id != efectivo.IdPg)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutEfectivo(efectivo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EfectivoExists(id))
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
        public async Task<ActionResult<Efectivo>> PostEfectivo(Efectivo efectivo)
        {
            try
            {
                await _service.PostEfectivo(efectivo);
            }
            catch (DbUpdateException)
            {
                if (EfectivoExists(efectivo.IdPg))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEfectivo", new { id = efectivo.IdPg }, efectivo);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteEfectivo(int id)
        {
            var efectivo = await _service.GetEfectivo(id);
            if (efectivo == null)
            {
                return NotFound();
            }

            await _service.DeleteEfectivo(id);
            return NoContent();
        }

        private bool EfectivoExists(int id)
        {
            return _service.GetEfectivo(id)!=null;
        }
    }   
}