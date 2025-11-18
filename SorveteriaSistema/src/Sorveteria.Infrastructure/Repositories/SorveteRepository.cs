using Microsoft.EntityFrameworkCore;
using Sorveteria.Domain.Entities;
using Sorveteria.Domain.Interfaces;
using Sorveteria.Infrastructure.Data;

namespace Sorveteria.Infrastructure.Repositories
{
    public class SorveteRepository : ISorveteRepository
    {
        private readonly SorveteriaDbContext _context;

        public SorveteRepository(SorveteriaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sorvete>> GetAllAsync()
        {
            return await _context.Sorvetes
                .Include(s => s.Categoria)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<Sorvete?> GetByIdAsync(int id)
        {
            return await _context.Sorvetes
                .Include(s => s.Categoria)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Sorvete>> GetByCategoriaIdAsync(int categoriaId)
        {
            return await _context.Sorvetes
                .Include(s => s.Categoria)
                .Where(s => s.CategoriaId == categoriaId)
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task AddAsync(Sorvete sorvete)
        {
            await _context.Sorvetes.AddAsync(sorvete);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sorvete sorvete)
        {
            _context.Sorvetes.Update(sorvete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sorvete = await GetByIdAsync(id);
            if (sorvete != null)
            {
                _context.Sorvetes.Remove(sorvete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Sorvete>> SearchAsync(string termo)
        {
            return await _context.Sorvetes
                .Include(s => s.Categoria)
                .Where(s => s.Nome.Contains(termo) || 
                           s.Sabor.Contains(termo) || 
                           s.Ingredientes.Contains(termo) ||
                           s.Categoria.Nome.Contains(termo))
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }


        public async Task<bool> ExisteNomeNaCategoriaAsync(string nome, int categoriaId, int? sorveteId = null)
        {
            var query = _context.Sorvetes
                .Where(s => s.Nome.ToLower() == nome.ToLower() && 
                           s.CategoriaId == categoriaId);

            if (sorveteId.HasValue)
            {
                query = query.Where(s => s.Id != sorveteId.Value);
            }

            return await query.AnyAsync();
        }
    }
}