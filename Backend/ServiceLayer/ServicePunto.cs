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
    public class ServicePunto 
    {
        private readonly CineContext _context;

        public ServicePunto(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Punto>> GetPuntos()
        {
            return await _context.Puntos.ToListAsync();
        }


        public async Task<Punto?> GetPunto(int id)
        {
            return await _context.Puntos.FindAsync(id);
        }


        public async Task PutPunto( Punto punto)
        {
            _context.Entry(punto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Punto> PostPunto(Punto punto)
        {
            _context.Puntos.Add(punto);
            await _context.SaveChangesAsync();
            
            return punto;
        }


        public async Task DeletePunto(int id)
        {
            var punto = await _context.Puntos.FindAsync(id);
            if (punto is not null)
            {
                _context.Puntos.Remove(punto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
