using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebImovelApi.Context;
using WebImovelApi.Entities;

namespace WebImovelApi.Services
{
    public class ImovelService
    {
        private readonly AppDbContext _context;

        public ImovelService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Imovel>> FindAllAsync()
        {
            var listImoveis = await _context.Imoveis.ToListAsync();

            return listImoveis;
        }

        public async Task<Imovel> FindByIdAsync(int id)
        {
            var imovel = await _context.Imoveis.FirstOrDefaultAsync(c => c.ImovelId == id);

            return imovel;
        }

        public async Task InsertAsync(Imovel imovel)
        {
            await _context.Imoveis.AddAsync(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            _context.Imoveis.Update(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);

            _context.Imoveis.Remove(imovel);
            await _context.SaveChangesAsync();
        }
    }
}
