using Sorveteria.Domain.Entities;

namespace Sorveteria.Domain.Interfaces
{
    public interface ISorveteRepository
    {
        Task<IEnumerable<Sorvete>> GetAllAsync();
        Task<Sorvete?> GetByIdAsync(int id);
        Task<IEnumerable<Sorvete>> GetByCategoriaIdAsync(int categoriaId);
        Task AddAsync(Sorvete sorvete);
        Task UpdateAsync(Sorvete sorvete);
        Task DeleteAsync(int id);
        Task<IEnumerable<Sorvete>> SearchAsync(string termo);
        Task<bool> ExisteNomeNaCategoriaAsync(string nome, int categoriaId, int? sorveteId = null);
    }
}