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
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).ToListAsync();
        }

        public async Task<Compra?> GetCompraByAll(int IdP, int IdS,string Ci, DateTime Fecha)
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).FirstOrDefaultAsync(x=>x.IdP==IdP && x.IdS==IdS && x.Ci==Ci && x.Fecha == Fecha);
        }

        public async Task<Compra?> GetCompra(int id)
        {

            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).FirstOrDefaultAsync(x=>x.IdP==id);
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


        public async Task DeleteCompra(int idPg)
        {
            var compra = await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).FirstOrDefaultAsync(x=>x.IdPg == idPg);
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
        }
    }
}