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
    public class ServiceSala 
    {
        private readonly CineContext _context;

        public ServiceSala(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Sala>> GetSalas()
        {
            return await _context.Salas.ToListAsync();
        }

        public async Task<Sala?> GetSala(int id)
        {
            return await _context.Salas.FindAsync(id);
        }


        public async Task PutSala( Sala sala)
        {
            _context.Entry(sala).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Sala> PostSala(Sala sala)
        {
            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            return  sala;
        }


        public async Task DeleteSala(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            if (sala is not null)
            {
                _context.Salas.Remove(sala);
                await _context.SaveChangesAsync();
            }
        }
    }
}
