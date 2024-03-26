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

    public class ServiceButaca : ControllerBase
    {
        private readonly CineContext _context;

        public ServiceButaca(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Butaca>> GetButacas()
        {
            return await _context.Butacas.ToListAsync();
        }


        public async Task<Butaca?> GetButaca(int id)
        {
            return await _context.Butacas.FindAsync(id);
        }


        public async Task PutButaca(Butaca butaca)
        {
            _context.Entry(butaca).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Butaca> PostButaca(Butaca butaca)
        {
            _context.Butacas.Add(butaca);
            await _context.SaveChangesAsync();
            return butaca;
        }


        public async Task DeleteButaca(int id)
        {
            var butaca = await _context.Butacas.FindAsync(id);
            if (butaca is not null)
            {
                _context.Butacas.Remove(butaca);
                await _context.SaveChangesAsync();
            }
        }
    }
}
