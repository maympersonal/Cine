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

    public class ServiceActor 
    {
        private readonly CineContext _context;

        public ServiceActor(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Actor>> GetActors()
        {
            return await _context.Actors.ToListAsync();
        }


        public async Task<Actor?> GetActor(int id)
        {
            return await _context.Actors.FindAsync(id);
        }


        public async Task PutActor(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Actor> PostActor(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            return actor;
        }


        public async Task DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor is not null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
