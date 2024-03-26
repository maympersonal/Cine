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
    public class ServiceCompra 
    {
        private readonly CineContext _context;

        public ServiceCompra(CineContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Compra>> GetCompras()
        {
            return await _context.Compras.ToListAsync();
        }


        public async Task<Compra?> GetCompra(int id)
        {

            return await _context.Compras.FindAsync(id);
        }


        public async Task PutCompra(Compra compra)
        {
            _context.Entry(compra).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Compra> PostCompra(Compra compra)
        {
            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
            return compra;
        }


        public async Task DeleteCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            if (compra is not null)
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
        }
    }
}
