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
    public class CompraController : ControllerBase
    {
        private readonly ServiceCompra _service;

        public CompraController(ServiceCompra service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            return Ok(await _service.GetCompras());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _service.GetCompra(id);

            if (compra == null)
            {
                return NotFound();
            }

            return compra;
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutCompra(int id, Compra compra)
        {
            if (id != compra.IdP)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutCompra(compra);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraExists(id))
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
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            try
            {
                await _service.PostCompra(compra);
            }
            catch (DbUpdateException)
            {
                if (CompraExists(compra.IdP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompra", new { id = compra.IdP }, compra);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCompra(int id)
        {
            var compra = await _service.GetCompra(id);
            if (compra == null)
            {
                return NotFound();
            }
            await _service.DeleteCompra(id);
            return NoContent();
        }

        private bool CompraExists(int id)
        {
            return _service.GetCompra(id)!=null;
        }
    } 
}