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
    public class ServiceTarjetum 
    {
        private readonly CineContext _context;

        public ServiceTarjetum(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Tarjetum>> GetTarjeta()
        {
            return await _context.Tarjeta.ToListAsync();
        }


        public async Task<Tarjetum?> GetTarjetum(string id)
        {
            return await _context.Tarjeta.FindAsync(id);
        }

        public async Task PutTarjetum( Tarjetum tarjetum)
        {
            _context.Entry(tarjetum).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Tarjetum> PostTarjetum(Tarjetum tarjetum)
        {
            _context.Tarjeta.Add(tarjetum);
            await _context.SaveChangesAsync();
           
            return tarjetum;
        }


        public async Task DeleteTarjetum(string id)
        {
            var tarjetum = await _context.Tarjeta.FindAsync(id);
            if (tarjetum is not null)
            {
                _context.Tarjeta.Remove(tarjetum);
                await _context.SaveChangesAsync();
            }
        }
    }
}
