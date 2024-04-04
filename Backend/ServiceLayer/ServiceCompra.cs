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

        public async Task<IEnumerable<Compra>> GetComprasByCliente(string Ci)
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).Where(x=>x.Ci==Ci).ToListAsync();
        }
        public async Task<IEnumerable<Compra>> GetComprasByPelicula(int IdP)
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).Where(x=>x.IdP==IdP).ToListAsync();
        }
        public async Task<IEnumerable<Compra>> GetComprasByTipo(string tipo)
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).Where(x=>x.Tipo==tipo).ToListAsync();
        }

        public async Task<IEnumerable<Compra>> GetComprasByFecha(DateTime inicio, DateTime final)
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).Where(x=>x.FechaDeCompra>=inicio&&x.FechaDeCompra<=final).ToListAsync();
        }

        public async Task<IEnumerable<Compra>> GetCompras()
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).ToListAsync();
        }

        public async Task<Compra?> GetCompraByAll(int IdP,int IdS,DateTime Fecha,string Ci,int IdPg)
        {
            return await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).FirstOrDefaultAsync(x=>x.IdP==IdP && x.IdS==IdS && x.Ci==Ci && x.Fecha == Fecha && x.IdPg==IdPg);
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


        public async Task DeleteCompra(int IdP,int IdS,DateTime Fecha,string Ci,int IdPg)
        {
            var compra = await _context.Compras.Include(x=>x.CiNavigation).Include(x=>x.IdPgNavigation).Include(x=>x.Sesion).Include(x=>x.IdBs).Include(x=>x.IdDs).FirstOrDefaultAsync(x=>x.IdPg == IdPg && x.IdP==IdP && x.IdS == IdS && x.Fecha == Fecha && x.Ci==Ci);
            if(compra is not null)
            {
                _context.Compras.Remove(compra);
                await _context.SaveChangesAsync();
            }
        }
    }
}