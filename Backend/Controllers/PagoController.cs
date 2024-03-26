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
    public class PagoController : ControllerBase
    {
        private readonly ServicePago _service;

        public PagoController (ServicePago service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return Ok(await _service.GetPagos());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _service.GetPago(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutPago(int id, Pago pago)
        {
            if (id != pago.IdPg)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutPago(pago);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            await _service.PostPago(pago);

            return CreatedAtAction("GetPago", new { id = pago.IdPg }, pago);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _service.GetPago(id);
            if (pago == null)
            {
                return NotFound();
            }

            await _service.DeletePago(id);
            return NoContent();
        }

        private bool PagoExists(int id)
        {
            return _service.GetPago(id)!=null;
        }
    }    
}