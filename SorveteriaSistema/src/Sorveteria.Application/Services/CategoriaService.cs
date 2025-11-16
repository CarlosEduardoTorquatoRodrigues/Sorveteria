using Mapster;
using Sorveteria.Application.Interfaces;
using Sorveteria.Application.ViewModels;
using Sorveteria.Domain.Entities;
using Sorveteria.Domain.Interfaces;

namespace Sorveteria.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<CategoriaViewModel>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Adapt<IEnumerable<CategoriaViewModel>>();
        }

        public async Task<CategoriaViewModel> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdWithSorvetesAsync(id);
            if (categoria == null) return null;

            var viewModel = categoria.Adapt<CategoriaViewModel>();
            viewModel.QuantidadeSorvetes = categoria.Sorvetes?.Count ?? 0;
            return viewModel;
        }

        public async Task AddAsync(CategoriaViewModel categoriaViewModel)
        {
            var categoria = categoriaViewModel.Adapt<Categoria>();
            categoria.DataCriacao = DateTime.Now;
            await _categoriaRepository.AddAsync(categoria);
        }

        public async Task UpdateAsync(CategoriaViewModel categoriaViewModel)
        {
            var categoria = categoriaViewModel.Adapt<Categoria>();
            await _categoriaRepository.UpdateAsync(categoria);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoriaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoriaViewModel>> SearchAsync(string termo)
        {
            var categorias = await _categoriaRepository.SearchAsync(termo);
            return categorias.Adapt<IEnumerable<CategoriaViewModel>>();
        }
    }
}