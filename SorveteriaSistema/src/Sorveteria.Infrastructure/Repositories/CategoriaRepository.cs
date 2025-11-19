using Microsoft.EntityFrameworkCore;
using Sorveteria.Domain.Entities;
using Sorveteria.Domain.Interfaces;
using Sorveteria.Infrastructure.Data;

namespace Sorveteria.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly SorveteriaDbContext _context;

        public CategoriaRepository(SorveteriaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias
                .Include(c => c.Sorvetes)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task<Categoria?> GetByIdAsync(int id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria?> GetByIdWithSorvetesAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.Sorvetes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await GetByIdAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Categoria>> SearchAsync(string termo)
        {
            return await _context.Categorias
                .Include(c => c.Sorvetes)
                .Where(c => c.Nome.Contains(termo) || c.Descricao.Contains(termo))
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }


        public async Task<bool> ExisteNomeAsync(string nome, int? categoriaId = null)
        {
            var query = _context.Categorias
                .Where(c => c.Nome.ToLower() == nome.ToLower());


            if (categoriaId.HasValue)
            {
                query = query.Where(c => c.Id != categoriaId.Value);
            }

            return await query.AnyAsync();
        }
    }
}