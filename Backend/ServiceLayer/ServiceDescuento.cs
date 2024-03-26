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
    public class ServiceDescuento 
    {
        private readonly CineContext _context;

        public ServiceDescuento(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Descuento>> GetDescuentos()
        {
            return await _context.Descuentos.ToListAsync();
        }

        public async Task<Descuento?> GetDescuento(int id)
        {
            return await _context.Descuentos.FindAsync(id);
        }

        public async Task PutDescuento(Descuento descuento)
        {
            _context.Entry(descuento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Descuento> PostDescuento(Descuento descuento)
        {
            _context.Descuentos.Add(descuento);
            await _context.SaveChangesAsync();
            return descuento;
        }


        public async Task DeleteDescuento(int id)
        {
            var descuento = await _context.Descuentos.FindAsync(id);
            if (descuento is not null)
            {
                _context.Descuentos.Remove(descuento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
