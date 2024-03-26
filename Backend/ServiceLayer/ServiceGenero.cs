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
    public class ServiceGenero 
    {
        private readonly CineContext _context;

        public ServiceGenero(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genero>> GetGeneros()
        {
            return await _context.Generos.ToListAsync();
        }


        public async Task<Genero?> GetGenero(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task PutGenero(Genero genero)
        {
            _context.Entry(genero).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Genero> PostGenero(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();

            return genero;
        }

        public async Task DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero is not null)
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
            }
        }
    }
}
