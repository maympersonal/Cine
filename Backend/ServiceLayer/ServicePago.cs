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
    public class ServicePago 
    {
        private readonly CineContext _context;

        public ServicePago(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> GetPagos()
        {
            return await _context.Pagos.Include(x=>x.Compras).Include(x=>x.Efectivo).Include(x=>x.Punto).Include(x=>x.Web).ToListAsync();
        }

        public async Task<Pago?> GetPago(int id)
        {
            return await _context.Pagos.Include(x=>x.Compras).Include(x=>x.Efectivo).Include(x=>x.Punto).Include(x=>x.Web).FirstOrDefaultAsync(x=>x.IdPg==id);
        }


        public async Task PutPago( Pago pago)
        {
            _context.Entry(pago).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Pago> PostPago(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return  pago;
        }


        public async Task DeletePago(int id)
        {
            var pago = await _context.Pagos.Include(x=>x.Compras).Include(x=>x.Efectivo).Include(x=>x.Punto).Include(x=>x.Web).FirstOrDefaultAsync(x=>x.IdPg==id);
            if (pago is not null)
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }
        }
    }
}
