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
    public class WebController : ControllerBase
    {
        private readonly ServiceWeb _service;

        public WebController (ServiceWeb service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Web>>> GetWebs()
        {
            return Ok(await _service.GetWebs());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Web>> GetWeb(int id)
        {
            var web = await _service.GetWeb(id);

            if (web == null)
            {
                return NotFound();
            }

            return web;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutWeb(int id, Web web)
        {
            if (id != web.IdPg)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutWeb(web);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebExists(id))
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
        public async Task<ActionResult<Web>> PostWeb(Web web)
        {
            try
            {
                await _service.PostWeb(web);
            }
            catch (DbUpdateException)
            {
                if (WebExists(web.IdPg))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeb", new { id = web.IdPg }, web);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteWeb(int id)
        {
            var web = await _service.GetWeb(id);
            if (web == null)
            {
                return NotFound();
            }

            await _service.DeleteWeb(id);
            return NoContent();
        }

        private bool WebExists(int id)
        {
            return _service.GetWeb(id)!=null;
        }
    }  
}