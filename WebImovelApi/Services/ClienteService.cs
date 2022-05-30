using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImovelApi.Context;
using WebImovelApi.Entities;

namespace WebImovelApi.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> FindAllAsync()
        {
            var listClientes = await _context.Clientes.ToListAsync();          

            return listClientes;
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);

            return cliente;
        }

        public async Task InsertAsync(Cliente cliente)
        {        
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
