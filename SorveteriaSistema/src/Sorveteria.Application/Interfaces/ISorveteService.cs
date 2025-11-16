using Sorveteria.Application.ViewModels;

namespace Sorveteria.Application.Interfaces
{
    public interface ISorveteService
    {
        Task<IEnumerable<SorveteViewModel>> GetAllAsync();
        Task<SorveteViewModel> GetByIdAsync(int id);
        Task<IEnumerable<SorveteViewModel>> GetByCategoriaIdAsync(int categoriaId);
        Task AddAsync(SorveteViewModel sorveteViewModel);
        Task UpdateAsync(SorveteViewModel sorveteViewModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<SorveteViewModel>> SearchAsync(string termo);
    }
}