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
        public async Task<IActionResult> PutCliente(string id, ClienteDtoIn cliente)
        {
            var Newcliente= await _service.GetCliente(id);
            if (id != cliente.Ci)
            {
                return BadRequest();
            }
            else if(Newcliente is not null)
            {
                Newcliente.Correo=cliente.Correo;
                Newcliente.Confiabilidad=cliente.Confiabilidad;
            }
            else return BadRequest();

            try
            {
                await _service.PutCliente(Newcliente);
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
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDtoIn cliente)
        {
            var Newcliente = new Cliente
            {
                Ci = cliente.Ci,
                Correo = cliente.Correo,
                Confiabilidad=true
            };
            try
            {
                await _service.PostCliente(Newcliente);
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