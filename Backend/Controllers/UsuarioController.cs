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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutUsuario(string id, UsuarioDtoIn usuario)
        {
            if (id != usuario.Ci)
            {
                return BadRequest();
            }
            var Oldcliente = await _servicecliente.GetCliente(id);
            var Oldusuario = await _serviceusuario.GetUsuario(id);
            if(Oldcliente is null || Oldusuario is null) return BadRequest();
            var Newcliente=new Cliente
            {
                Ci=Oldcliente.Ci,
                Correo=usuario.Correo,
                Confiabilidad=Oldcliente.Confiabilidad,
                Tarjeta=Oldcliente.Tarjeta,
                Compras=Oldcliente.Compras
            };
            var NewUsuario = new Usuario
            {
                Ci=Oldusuario.Ci,
                NombreS=usuario.NombreS,
                Apellidos=usuario.Apellidos,
                Puntos=Oldusuario.Puntos,
                Codigo=Oldusuario.Codigo,
                Contrasena=GenerarHashSHA256(usuario.Contrasena),
                Rol=Oldusuario.Rol,
                CiNavigation=Newcliente
            };
            NewUsuario.CiNavigation.Usuario=NewUsuario;
            try
            {
                await _serviceusuario.PutUsuario(NewUsuario);
                await _servicecliente.PutCliente(Newcliente);
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
                usuario1.CiNavigation.Usuario=usuario1;
                newcliente.Usuario=usuario1;
                await _servicecliente.PostCliente(newcliente);
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

