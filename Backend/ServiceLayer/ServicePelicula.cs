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
            return await _context.Peliculas.Include(x=>x.IdAs).Include(x=>x.IdGs).ToListAsync();
        }


        public async Task<Pelicula?> GetPelicula(int id)
        {
            return await _context.Peliculas.Include(x=>x.IdGs).Include(x=>x.IdAs).FirstOrDefaultAsync(x=>x.IdP==id);
        }


        public async Task PutPelicula( Pelicula pelicula)
        {
            // Actualizar la entidad principal (Pelicula)
            _context.Entry(pelicula).State = EntityState.Modified;

            // Actualizar las relaciones de la película con los actores y géneros
            _context.Entry(pelicula).Collection(p => p.IdAs).IsModified = true;
            _context.Entry(pelicula).Collection(p => p.IdGs).IsModified = true;

            try
            {
                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Manejar la excepción de concurrencia
                throw;
            }
            catch (DbUpdateException ex)
            {
                // Manejar la excepción de violación de restricción de clave primaria
                // Aquí puedes analizar el mensaje de error o realizar acciones específicas según tus necesidades
                throw new Exception("Error al actualizar la película. " + ex.Message);
            }
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
