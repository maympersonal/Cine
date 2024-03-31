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
    public class ServicePelicula 
    {
        private readonly CineContext _context;

        public ServicePelicula(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Pelicula>> GetPeliculas()
        {
            return await _context.Peliculas.ToListAsync();
        }


        public async Task<Pelicula?> GetPelicula(int id)
        {
            return await _context.Peliculas.FindAsync(id);
        }


        public async Task PutPelicula( Pelicula pelicula)
        {
            _context.Entry(pelicula).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Pelicula> PostPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return pelicula;
        }


        public async Task DeletePelicula(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula is not null)
            {
                _context.Peliculas.Remove(pelicula);
                await _context.SaveChangesAsync();
            }
        }
    }
}
