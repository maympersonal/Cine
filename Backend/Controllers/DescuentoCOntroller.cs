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
    public class DescuentoController : ControllerBase
    {
        private readonly ServiceDescuento _service;

        public DescuentoController (ServiceDescuento service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Descuento>>> GetDescuentos()
        {
            return Ok(await _service.GetDescuentos());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Descuento>> GetDescuento(int id)
        {
            var descuento = await _service.GetDescuento(id);

            if (descuento == null)
            {
                return NotFound();
            }

            return descuento;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutDescuento(int id, Descuento descuento)
        {
            if (id != descuento.IdD)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutDescuento(descuento);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescuentoExists(id))
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
        public async Task<ActionResult<Descuento>> PostDescuento(Descuento descuento)
        {
            await _service.PostDescuento(descuento);
            return CreatedAtAction("GetDescuento", new { id = descuento.IdD }, descuento);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDescuento(int id)
        {
            var descuento = await _service.GetDescuento(id);
            if (descuento == null)
            {
                return NotFound();
            }

            await _service.DeleteDescuento(id);
            return NoContent();
        }

        private bool DescuentoExists(int id)
        {
            return _service.GetDescuento(id)!=null;
        }
    }   
}