using Microsoft.AspNetCore.Mvc;
using Sorveteria.Application.Interfaces;
using Sorveteria.Application.ViewModels;

namespace Sorveteria.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return View(categorias);
        }

        public async Task<IActionResult> Details(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoriaViewModel);
            }


            var jaExiste = await _categoriaService.ExisteNomeAsync(categoriaViewModel.Nome);

            if (jaExiste)
            {
                ModelState.AddModelError("Nome", "Já existe uma categoria com este nome.");
                return View(categoriaViewModel);
            }

            try
            {
                await _categoriaService.AddAsync(categoriaViewModel);
                TempData["Success"] = "Categoria criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Nome", ex.Message);
                return View(categoriaViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao criar categoria: {ex.Message}";
                return View(categoriaViewModel);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(categoriaViewModel);
            }


            var jaExiste = await _categoriaService.ExisteNomeAsync(
                categoriaViewModel.Nome,
                categoriaViewModel.Id
            );

            if (jaExiste)
            {
                ModelState.AddModelError("Nome", "Já existe outra categoria com este nome.");
                return View(categoriaViewModel);
            }

            try
            {
                await _categoriaService.UpdateAsync(categoriaViewModel);
                TempData["Success"] = "Categoria atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("Nome", ex.Message);
                return View(categoriaViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao atualizar categoria: {ex.Message}";
                return View(categoriaViewModel);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoriaService.DeleteAsync(id);
                TempData["Success"] = "Categoria excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao excluir categoria: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                var todasCategorias = await _categoriaService.GetAllAsync();
                return PartialView("_CategoriasList", todasCategorias);
            }

            var categorias = await _categoriaService.SearchAsync(termo);
            return PartialView("_CategoriasList", categorias);
        }

        [HttpGet]
        public async Task<JsonResult> ValidarNomeUnico(string nome, int? id = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return Json(new { valido = true });
            }

            var jaExiste = await _categoriaService.ExisteNomeAsync(nome, id);

            return Json(new
            {
                valido = !jaExiste,
                mensagem = jaExiste ? "Já existe uma categoria com este nome." : ""
            });
        }
    }
}