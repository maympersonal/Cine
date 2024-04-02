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
    public class ServiceEfectivo
    {
        private readonly CineContext _context;

        public ServiceEfectivo(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Efectivo>> GetEfectivos()
        {
            return await _context.Efectivos.Include(x=>x.IdPgNavigation).ToListAsync();
        }

        public async Task<Efectivo?> GetEfectivo(int id)
        {
            return await _context.Efectivos.Include(x=>x.IdPgNavigation).FirstOrDefaultAsync(x=>x.IdPg==id);
        }

        public async Task PutEfectivo(Efectivo efectivo)
        {
            _context.Entry(efectivo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Efectivo> PostEfectivo(Efectivo efectivo)
        {
            _context.Efectivos.Add(efectivo);
            await _context.SaveChangesAsync();
            return efectivo;
        }


        public async Task DeleteEfectivo(int id)
        {
            var efectivo = await _context.Efectivos.Include(x=>x.IdPgNavigation).FirstOrDefaultAsync(x=>x.IdPg==id);
            if (efectivo is not null)
            {
                _context.Efectivos.Remove(efectivo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
