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
using Backend.Settings;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ServiceUsuario _serviceusuario;
        private readonly ServiceCliente _servicecliente;

        public UsuarioController (ServiceUsuario serviceusuario,ServiceCliente servicecliente)
        {
            _serviceusuario = serviceusuario;
            _servicecliente =servicecliente;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return Ok(await _serviceusuario.GetUsuarios());
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _serviceusuario.GetUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("LogUser")]
        public async Task<ActionResult<string>> LogUsuario(LogDtoIn UserLog)
        {
            var usuario = await _serviceusuario.GetUsuario(UserLog.Ci);
            if(usuario is null) return NotFound();

            if(usuario.Contrasena==GenerarHashSHA256(UserLog.Contrasena)) return Ok(usuario.Rol);

            return BadRequest();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutUsuario(string id, UsuarioDtoIn usuario)
        {
            if (id != usuario.Ci)
            {
                return BadRequest();
            }

            var existingCliente = await _servicecliente.GetCliente(id);
            var existingUsuario = await _serviceusuario.GetUsuario(id);

            if (existingCliente is null || existingUsuario is null)
            {
                return BadRequest();
            }

            existingCliente.Correo = usuario.Correo;
            existingUsuario.NombreS = usuario.NombreS;
            existingUsuario.Apellidos = usuario.Apellidos;
            existingUsuario.Contrasena = GenerarHashSHA256(usuario.Contrasena);

            try
            {
                await _servicecliente.PutCliente(existingCliente);
                await _serviceusuario.PutUsuario(existingUsuario);
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
        public async Task<ActionResult<Usuario>> PostUsuario(UsuarioDtoIn usuario)
        {
            var newcliente= await _servicecliente.GetCliente(usuario.Ci);
            string codigo = Settings.Settings.GenerarCodigo();

            while(await _serviceusuario.ExistUserCode(codigo))
            {
                codigo=Settings.Settings.GenerarCodigo();
            }

            if(newcliente is null)
            {
                newcliente = new Cliente
                {
                    Ci=usuario.Ci,
                    Correo=usuario.Correo,
                    Confiabilidad=true
                };
            }
            try
            {

                Usuario usuario1=new Usuario
                {
                    Ci=usuario.Ci,
                    Apellidos=usuario.Apellidos,
                    NombreS=usuario.NombreS,
                    Puntos=0,
                    Rol="Cliente",
                    CiNavigation=newcliente,
                    Codigo=codigo,
                    Contrasena= GenerarHashSHA256(usuario.Contrasena)
                };
                await _serviceusuario.PostUsuario(usuario1);
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
            var usuario = await _serviceusuario.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            await _serviceusuario.DeleteUsuario(id);
            return NoContent();
        }

        private bool UsuarioExists(string id)
        {
            return _serviceusuario.GetUsuario(id)!=null;
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

