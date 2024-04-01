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
    public class SalaController : ControllerBase
    {
        private readonly ServiceSala _service;

        public SalaController (ServiceSala service)
        {
            _service = service;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
            return Ok(await _service.GetSalas());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _service.GetSala(id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutSala(int id, SalaDtoIn sala)
        {
            var Oldsalsa = await _service.GetSala(id);
            if (Oldsalsa is null)
            {
                return BadRequest();
            }
            Oldsalsa.Capacidad=sala.Capacidad;
            try
            {
                await _service.PutSala(Oldsalsa);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(id))
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
        public async Task<ActionResult<Sala>> PostSala(SalaDtoIn sala)
        {
            var Newsala= new Sala{Capacidad=sala.Capacidad};
            await _service.PostSala(Newsala);

            return CreatedAtAction("GetSala", new { id = Newsala.IdS }, Newsala);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            var sala = await _service.GetSala(id);
            if (sala == null)
            {
                return NotFound();
            }

            await _service.DeleteSala(id);
            return NoContent();
        }

        private bool SalaExists(int id)
        {
            return _service.GetSala(id)!=null;
        }
    }   
}