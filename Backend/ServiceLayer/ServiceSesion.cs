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
    public class ServiceSesion 
    {
        private readonly CineContext _context;

        public ServiceSesion(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Sesion>> GetSesions()
        {
            return await _context.Sesions.ToListAsync();
        }


        public async Task<Sesion?> GetSesion(int id)
        {
            return await _context.Sesions.FindAsync(id);
        }

        public async Task PutSesion(Sesion sesion)
        {
            _context.Entry(sesion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Sesion> PostSesion(Sesion sesion)
        {
            _context.Sesions.Add(sesion);
            await _context.SaveChangesAsync();

            return sesion;
        }


        public async Task DeleteSesion(int id)
        {
            var sesion = await _context.Sesions.FindAsync(id);
            if (sesion is not null)
            {
                _context.Sesions.Remove(sesion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
