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
    public class ServiceCliente : ControllerBase
    {
        private readonly CineContext _context;

        public ServiceCliente(CineContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetCliente(string id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task PutCliente(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task DeleteCliente(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente is not null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
