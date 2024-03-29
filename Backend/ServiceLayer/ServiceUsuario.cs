using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.ServiceLayer
{
    public class ServiceUsuario 
    {
        private readonly CineContext _context;

        public ServiceUsuario(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetUsuario(string id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<bool> ExistUserCode(string code)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(a => a.Codigo == code);
            return user != null;
        }


        public async Task PutUsuario( Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return  usuario;
        }

        public async Task DeleteUsuario(string id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario is not null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
