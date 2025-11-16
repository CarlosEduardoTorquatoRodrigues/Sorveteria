using Mapster;
using Sorveteria.Application.Interfaces;
using Sorveteria.Application.ViewModels;
using Sorveteria.Domain.Entities;
using Sorveteria.Domain.Interfaces;

namespace Sorveteria.Application.Services
{
    public class SorveteService : ISorveteService
    {
        private readonly ISorveteRepository _sorveteRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public SorveteService(ISorveteRepository sorveteRepository, ICategoriaRepository categoriaRepository)
        {
            _sorveteRepository = sorveteRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<SorveteViewModel>> GetAllAsync()
        {
            var sorvetes = await _sorveteRepository.GetAllAsync();
            var viewModels = new List<SorveteViewModel>();
            
            foreach (var sorvete in sorvetes)
            {
                var vm = sorvete.Adapt<SorveteViewModel>();
                vm.CategoriaNome = sorvete.Categoria?.Nome ?? "Sem Categoria";
                viewModels.Add(vm);
            }
            
            return viewModels;
        }

        public async Task<SorveteViewModel> GetByIdAsync(int id)
        {
            var sorvete = await _sorveteRepository.GetByIdAsync(id);
            if (sorvete == null) return null;
            
            var vm = sorvete.Adapt<SorveteViewModel>();
            vm.CategoriaNome = sorvete.Categoria?.Nome ?? "Sem Categoria";
            return vm;
        }

        public async Task<IEnumerable<SorveteViewModel>> GetByCategoriaIdAsync(int categoriaId)
        {
            var sorvetes = await _sorveteRepository.GetByCategoriaIdAsync(categoriaId);
            var viewModels = new List<SorveteViewModel>();
            
            foreach (var sorvete in sorvetes)
            {
                var vm = sorvete.Adapt<SorveteViewModel>();
                vm.CategoriaNome = sorvete.Categoria?.Nome ?? "Sem Categoria";
                viewModels.Add(vm);
            }
            
            return viewModels;
        }

        public async Task AddAsync(SorveteViewModel sorveteViewModel)
        {
            var sorvete = sorveteViewModel.Adapt<Sorvete>();
            sorvete.DataCriacao = DateTime.Now;
            await _sorveteRepository.AddAsync(sorvete);
        }

        public async Task UpdateAsync(SorveteViewModel sorveteViewModel)
        {
            var sorvete = sorveteViewModel.Adapt<Sorvete>();
            await _sorveteRepository.UpdateAsync(sorvete);
        }

        public async Task DeleteAsync(int id)
        {
            await _sorveteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SorveteViewModel>> SearchAsync(string termo)
        {
            var sorvetes = await _sorveteRepository.SearchAsync(termo);
            var viewModels = new List<SorveteViewModel>();
            
            foreach (var sorvete in sorvetes)
            {
                var vm = sorvete.Adapt<SorveteViewModel>();
                vm.CategoriaNome = sorvete.Categoria?.Nome ?? "Sem Categoria";
                viewModels.Add(vm);
            }
            
            return viewModels;
        }
    }
}