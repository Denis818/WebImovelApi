using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebImovelApi.Context;
using WebImovelApi.Entities;

namespace WebImovelApi.Services
{
    public class LocadorService
    {
        private readonly AppDbContext _context;

        public LocadorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Locador>> FindAllAsync()
        {
            var listLocadores = await _context.Locadores.ToListAsync();

            return listLocadores;
        }

        public async Task<Locador> FindByIdAsync(int id)
        {
            var locador = await _context.Locadores.FirstOrDefaultAsync(l => l.LocadorId == id);

            return locador;
        }

        public async Task InsertAsync(Locador locador)
        {
            await _context.Locadores.AddAsync(locador);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Locador locador)
        {
            _context.Locadores.Update(locador);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var locador = await _context.Locadores.FindAsync(id);

            _context.Locadores.Remove(locador);
            await _context.SaveChangesAsync();
        }
    }
}
