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
    public class ServiceWeb 
    {
        private readonly CineContext _context;

        public ServiceWeb(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Web>> GetWebs()
        {
            return await _context.Webs.Include(x=>x.CodigoTNavigation).Include(x=>x.IdPgNavigation).ToListAsync();
        }


        public async Task<Web?> GetWeb(int id)
        {
            return await _context.Webs.Include(x=>x.CodigoTNavigation).Include(x=>x.IdPgNavigation).FirstOrDefaultAsync(x=>x.IdPg==id);
        }

        public async Task PutWeb( Web web)
        {
            _context.Entry(web).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Web> PostWeb(Web web)
        {
            _context.Webs.Add(web);
            await _context.SaveChangesAsync();
            
            return  web;
        }


        public async Task DeleteWeb(int id)
        {
            var web = await _context.Webs.Include(x=>x.CodigoTNavigation).Include(x=>x.IdPgNavigation).FirstOrDefaultAsync(x=>x.IdPg==id);
            if (web is not null)
            {
                _context.Webs.Remove(web);
                await _context.SaveChangesAsync();
            }
        }
    }
}
