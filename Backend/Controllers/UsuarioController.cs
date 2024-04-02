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

        [HttpPost("LogUser")]
        public async Task<ActionResult<Usuario>> LogUsuario(LogDtoIn UserLog)
        {
            var usuario = await _serviceusuario.GetUsuario(UserLog.Ci);
            if(usuario is null) return NotFound();
            if(CompareByteArrays(usuario.Contrasena,GenerarHashSHA256(UserLog.Contrasena))) return Ok(usuario);
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

        [HttpPut("ChangeToSocio/{id}")]
        public async Task<IActionResult> ChangeToSocio ( string id)
        {
            var Usuario = await _serviceusuario.GetUsuario(id);

            if (Usuario is null)
            {
                return NotFound();
            }

            try
            {
                await _serviceusuario.PutUsuario(Usuario);
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
        [HttpPut("ChangeRol/{id}/{rol}")]
        public async Task<IActionResult> ChangeToSocio ( string id,string rol)
        {
            var Usuario = await _serviceusuario.GetUsuario(id);

            if (Usuario is null)
            {
                return NotFound();
            }
            Usuario.Rol=rol;
            try
            {
                await _serviceusuario.PutUsuario(Usuario);
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
            try
            {
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

            return CreatedAtAction("GetUsuario", new { id = usuario1.Ci }, usuario1);
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

        private byte[] GenerarHashSHA256(string contraseña)
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
       private bool CompareByteArrays(byte[] arr1, byte[] arr2) {
            if (arr1.Length != arr2.Length) {
                return false;
                }

            for (int i = 0; i < arr1.Length; i++) {
                if (arr1[i] != arr2[i]) {
                    return false;
                }
            }

            return true;
        }

    }  
}