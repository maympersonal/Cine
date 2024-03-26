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
    public class TarjetumController : ControllerBase
    {
        private readonly ServiceTarjetum _service;

        public TarjetumController (ServiceTarjetum service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Tarjetum>>> GetTarjeta()
        {
            return Ok(await _service.GetTarjeta());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Tarjetum>> GetTarjetum(string id)
        {
            var tarjetum = await _service.GetTarjetum(id);

            if (tarjetum == null)
            {
                return NotFound();
            }

            return tarjetum;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutTarjetum(string id, Tarjetum tarjetum)
        {
            if (id != tarjetum.CodigoT)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutTarjetum(tarjetum);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetumExists(id))
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
        public async Task<ActionResult<Tarjetum>> PostTarjetum(Tarjetum tarjetum)
        {
            try
            {
                await _service.PostTarjetum(tarjetum);
            }
            catch (DbUpdateException)
            {
                if (TarjetumExists(tarjetum.CodigoT))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTarjetum", new { id = tarjetum.CodigoT }, tarjetum);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTarjetum(string id)
        {
            var tarjetum = await _service.GetTarjetum(id);
            if (tarjetum == null)
            {
                return NotFound();
            }

            await _service.DeleteTarjetum(id);
            return NoContent();
        }

        private bool TarjetumExists(string id)
        {
            return _service.GetTarjetum(id)!=null;
        }
    }   
}