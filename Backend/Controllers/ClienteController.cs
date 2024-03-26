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
    public class ClienteController : ControllerBase
    {
        private readonly ServiceCliente _service;

        public ClienteController(ServiceCliente service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return Ok(await _service.GetClientes());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            var cliente = await _service.GetCliente(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            if (id != cliente.Ci)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutCliente(cliente);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            try
            {
                await _service.PostCliente(cliente);
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.Ci))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.Ci }, cliente);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            var cliente = await _service.GetCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            await _service.DeleteCliente(id);
            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return _service.GetCliente(id)!=null;
        }
    }   
}