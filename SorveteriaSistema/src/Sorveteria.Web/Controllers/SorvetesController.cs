using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sorveteria.Application.Interfaces;
using Sorveteria.Application.ViewModels;

namespace Sorveteria.Web.Controllers
{
    public class SorvetesController : Controller
    {
        private readonly ISorveteService _sorveteService;
        private readonly ICategoriaService _categoriaService;

        public SorvetesController(ISorveteService sorveteService, ICategoriaService categoriaService)
        {
            _sorveteService = sorveteService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var sorvetes = await _sorveteService.GetAllAsync();
            return View(sorvetes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sorvete = await _sorveteService.GetByIdAsync(id);
            if (sorvete == null)
            {
                return NotFound();
            }
            return View(sorvete);
        }

        public async Task<IActionResult> Create()
        {
            await CarregarCategorias();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SorveteViewModel sorveteViewModel)
        {
            Console.WriteLine("=== CREATE POST CHAMADO ===");
            Console.WriteLine($"Nome: {sorveteViewModel?.Nome}");
            Console.WriteLine($"Sabor: {sorveteViewModel?.Sabor}");
            Console.WriteLine($"Preco: {sorveteViewModel?.Preco}");
            Console.WriteLine($"QuantidadeEstoque: {sorveteViewModel?.QuantidadeEstoque}");
            Console.WriteLine($"CategoriaId: {sorveteViewModel?.CategoriaId}");
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("=== ERROS DE VALIDAÇÃO ===");
                var errors = new List<string>();
                
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        foreach (var error in state.Errors)
                        {
                            var errorMsg = $"{key}: {error.ErrorMessage}";
                            Console.WriteLine(errorMsg);
                            errors.Add(errorMsg);
                            
                            if (error.Exception != null)
                            {
                                Console.WriteLine($"  Exception: {error.Exception.Message}");
                            }
                        }
                    }
                }
                
                ViewBag.ErrorMessage = string.Join(" | ", errors);
                await CarregarCategorias();
                return View(sorveteViewModel);
            }


            var jaExiste = await _sorveteService.ExisteNomeNaCategoriaAsync(
                sorveteViewModel.Nome, 
                sorveteViewModel.CategoriaId
            );

            if (jaExiste)
            {
                ModelState.AddModelError("Nome", "Já existe um sorvete com este nome nesta categoria.");
                ViewBag.ErrorMessage = "Já existe um sorvete com este nome nesta categoria.";
                await CarregarCategorias();
                return View(sorveteViewModel);
            }

            try
            {
                Console.WriteLine("=== TENTANDO SALVAR ===");
                await _sorveteService.AddAsync(sorveteViewModel);
                Console.WriteLine("=== SALVO COM SUCESSO ===");
                TempData["Success"] = "Sorvete criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine($"=== ERRO DE VALIDAÇÃO DE NEGÓCIO ===");
                Console.WriteLine($"Message: {ex.Message}");
                ModelState.AddModelError("Nome", ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                await CarregarCategorias();
                return View(sorveteViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"=== ERRO AO SALVAR ===");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {ex.InnerException.Message}");
                }
                
                TempData["Error"] = $"Erro ao criar sorvete: {ex.Message}";
                await CarregarCategorias();
                return View(sorveteViewModel);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sorvete = await _sorveteService.GetByIdAsync(id);
            if (sorvete == null)
            {
                return NotFound();
            }
            await CarregarCategorias();
            return View(sorvete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SorveteViewModel sorveteViewModel)
        {
            if (id != sorveteViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                
                ViewBag.ErrorMessage = string.Join(" | ", errors);
                await CarregarCategorias();
                return View(sorveteViewModel);
            }


            var jaExiste = await _sorveteService.ExisteNomeNaCategoriaAsync(
                sorveteViewModel.Nome, 
                sorveteViewModel.CategoriaId,
                sorveteViewModel.Id 
            );

            if (jaExiste)
            {
                ModelState.AddModelError("Nome", "Já existe outro sorvete com este nome nesta categoria.");
                ViewBag.ErrorMessage = "Já existe outro sorvete com este nome nesta categoria.";
                await CarregarCategorias();
                return View(sorveteViewModel);
            }

            try
            {
                await _sorveteService.UpdateAsync(sorveteViewModel);
                TempData["Success"] = "Sorvete atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                ModelState.AddModelError("Nome", ex.Message);
                ViewBag.ErrorMessage = ex.Message;
                await CarregarCategorias();
                return View(sorveteViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao atualizar sorvete: {ex.Message}";
                await CarregarCategorias();
                return View(sorveteViewModel);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sorvete = await _sorveteService.GetByIdAsync(id);
            if (sorvete == null)
            {
                return NotFound();
            }
            return View(sorvete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _sorveteService.DeleteAsync(id);
                TempData["Success"] = "Sorvete excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao excluir sorvete: {ex.Message}";
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                var todosSorvetes = await _sorveteService.GetAllAsync();
                return PartialView("_SorvetesList", todosSorvetes);
            }

            var sorvetes = await _sorveteService.SearchAsync(termo);
            return PartialView("_SorvetesList", sorvetes);
        }


        [HttpGet]
        public async Task<JsonResult> ValidarNomeUnico(string nome, int categoriaId, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(nome) || categoriaId == 0)
            {
                return Json(new { valido = true });
            }

            var jaExiste = await _sorveteService.ExisteNomeNaCategoriaAsync(nome, categoriaId, id);
            
            return Json(new 
            { 
                valido = !jaExiste,
                mensagem = jaExiste ? "Já existe um sorvete com este nome nesta categoria." : ""
            });
        }

        private async Task CarregarCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();
            
            Console.WriteLine($"=== CATEGORIAS CARREGADAS: {categorias.Count()} ===");
            foreach (var cat in categorias)
            {
                Console.WriteLine($"  - Id: {cat.Id}, Nome: {cat.Nome}");
            }
            
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
        }
    }
}