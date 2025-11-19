using Sorveteria.Domain.Entities;

namespace Sorveteria.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria?> GetByIdAsync(int id);
        Task<Categoria?> GetByIdWithSorvetesAsync(int id);
        Task AddAsync(Categoria categoria);
        Task UpdateAsync(Categoria categoria);
        Task DeleteAsync(int id);
        Task<IEnumerable<Categoria>> SearchAsync(string termo);
        

        Task<bool> ExisteNomeAsync(string nome, int? categoriaId = null);
    }
}