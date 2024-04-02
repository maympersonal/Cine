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
    public class ButacaController : ControllerBase
    {
        private readonly ServiceButaca _service;

        public ButacaController (ServiceButaca service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Butaca>>> GetButacas()
        {
            return Ok(await _service.GetButacas());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Butaca>> GetButaca(int id)
        {
            var butaca = await _service.GetButaca(id);

            if (butaca == null)
            {
                return NotFound();
            }

            return butaca;
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutButaca(int id, ButacaDtoIn butaca)
        {
            var Oldbutaca= await _service.GetButaca(id);
            if ( Oldbutaca is null)
            {
                return BadRequest();
            }
            Oldbutaca.IdS=butaca.IdS;
            try
            {
                await _service.PutButaca(Oldbutaca);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ButacaExists(id))
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
        public async Task<ActionResult<Butaca>> PostButaca(ButacaDtoIn butaca)
        {
            var Newbutaca=new Butaca
            {
                IdS=butaca.IdS
            };
            await _service.PostButaca(Newbutaca);
            return CreatedAtAction("GetButaca", new { id = Newbutaca.IdB }, Newbutaca);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteButaca(int id)
        {
            var actor = await _service.GetButaca(id);
            if (actor == null)
            {
                return NotFound();
            }
            await _service.DeleteButaca(id);
            return NoContent();
        }

        private bool ButacaExists(int id)
        {
            return _service.GetButaca(id) != null;
        }
    }
}