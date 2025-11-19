using Sorveteria.Application.ViewModels;

namespace Sorveteria.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> GetAllAsync();
        Task<CategoriaViewModel?> GetByIdAsync(int id);
        Task AddAsync(CategoriaViewModel categoriaViewModel);
        Task UpdateAsync(CategoriaViewModel categoriaViewModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<CategoriaViewModel>> SearchAsync(string termo);
        Task<bool> ExisteNomeAsync(string nome, int? categoriaId = null);
    }
}