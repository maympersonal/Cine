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
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ServiceUsuario _service;

        public UsuarioController (ServiceUsuario service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return Ok(await _service.GetUsuarios());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _service.GetUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutUsuario(string id, Usuario usuario)
        {
            if (id != usuario.Ci)
            {
                return BadRequest();
            }

            try
            {
                await _service.PutUsuario(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDto usuario)
        {
            try
            {
                Usuario usuario1=new Usuario();
                usuario1.Ci=usuario.Ci;
                usuario1.Apellidos=usuario.Apellidos;
                usuario1.Codigo=usuario.Codigo;
                usuario1.NombreS=usuario.NombreS;
                usuario1.Rol=usuario.Rol;
                usuario1.Puntos=0;
                usuario1.Contrasena= GenerarHashSHA256(usuario.Contrasena);
                await _service.PostUsuario(usuario1);
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.Ci))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuario", new { id = usuario.Ci }, usuario);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            var usuario = await _service.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _service.DeleteUsuario(id);
            return NoContent();
        }

        private bool UsuarioExists(string id)
        {
            return _service.GetUsuario(id)!=null;
        }

        static byte[] GenerarHashSHA256(string contraseña)
        {
            // Crear una instancia de SHA-256
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña en una matriz de bytes utilizando UTF-8
                byte[] bytesContraseña = Encoding.UTF8.GetBytes(contraseña);
                
                // Calcular el hash de la contraseña
                byte[] hashBytes = sha256.ComputeHash(bytesContraseña);
                
                // Devolver el hash 
                return hashBytes;
            }
        }

    }  
}

